using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelBase;
using LissovBase;
using LissovLog;

namespace GeneralCVSModel
{
    internal class ModelValidator
    {
        GeneralModel _model;
        public ModelValidator(GeneralModel model)
        {
            _model = model;
        }

        public LissovBase.LissovModelBase.ValidateResult ValidateModels()
        {
            var result = new LissovBase.LissovModelBase.ValidateResult()
            {
                ErrorCount = 0,
                WarnCount = 0
            };

            #region Links
            //list of inputs
            var usedOutputs = new Dictionary<LinkInfo, bool>();
            foreach (var model in _model.Configuration.Models)
            {
                foreach (var value in model.GetValues())
                {
                    if (value.Type == Value.ValueType.Input)
                    {
                        usedOutputs[new LinkInfo(value.LinkModelName, value.LinkValueName)] = false;
                    }
                }
            }

            foreach (var model in _model.Configuration.Models)
            {
                foreach (var value in model.GetValues())
                {
                    if (value.Type == Value.ValueType.Output)
                    {
                        LinkInfo item = new LinkInfo(model.GetName(), value.Name);
                        if (usedOutputs.ContainsKey(item))
                        {
                            usedOutputs[item] = true;
                        }
                        else
                        {
                            if (value is LissovValue)
                            {
                                if ((value as LissovValue).LinkExpected)
                                {
                                    Log.Write(LogLevel.WARN, "Output Value [{0} -> {1}] is not linked to Input value", model.GetName(), value.DisplayName);
                                    result.WarnCount++;
                                }
                            }
                        }
                    }
                }
            }

            //inform about wrong links
            foreach (var item in usedOutputs)
            {
                if (!item.Value)
                {
                    Log.Write(LogLevel.ERROR, "Input Value [{0} -> {1}] is linked to non-existing output value", item.Key.Model, item.Key.Value);
                    result.ErrorCount++;
                }
            } 
            #endregion

            foreach (var item in _model.Configuration.Models)
            {
                if (item != _model && _model is LissovModelBase)
                {
                    result += (item as LissovModelBase).Validate();
                }
            }
            
            //Step research
            Log.Write(LogLevel.INFO, "Maximum step recommended: {0}", _model.GetMaxStepInfo().Text);

            Log.Write(LogLevel.INFO, "Models validated. Erros: {0}, Warnings: {1}", result.ErrorCount, result.WarnCount);

            return result;
        }

        private struct LinkInfo
        {
            public string Model;
            public string Value;
            public LinkInfo(string model, string value)
            {
                Model = model;
                Value = value;
            }
        }
    }
}
