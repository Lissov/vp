using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelBase;

namespace LissovBase
{
    public interface IModelSetupControl
    {
        void Update();

        IModel Model { get; set; }

        event EventHandler OnUpdate;

        Type GetModelType();
    }
}
