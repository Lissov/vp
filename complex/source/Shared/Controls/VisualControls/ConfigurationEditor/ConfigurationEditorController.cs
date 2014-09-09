using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace VisualControls.ConfigurationEditor
{
    public class ConfigurationEditorController
    {
        private Configuration _config;
        protected ConfigurationEditor _editor;

        protected Vertex _activeVertex = null;
        protected Link _activeLink = null;
        protected int _vertexHitX;
        protected int _vertexHitY;

        #region Constants
        public int VERTEX_RADIUS = 40;
        private const int ARROW_SIZE = 20;
        private const double ARROW_ANGLE = 0.3;
        private const int VERTEX_REALHEIGHT_RADIUS = 5;
        private const double LINK_HIT_PRECISION = 4;

        private Pen vertexPen = new Pen(Color.Black, 1);
        private Pen vertexRealHeightPen = new Pen(Color.Red, 2);
        public Font vertexFont = new Font(FontFamily.GenericSerif, 12, FontStyle.Italic);
        private Pen linkPen = new Pen(Color.Gray, 2);
        private Pen linkPenActive = new Pen(Color.OrangeRed, 3);
        private Brush activeVertexBrush = Brushes.Red;
        private Brush normalVertexBrush = Brushes.White;
        private Brush vertexRealHeightBrush = Brushes.Red;
        private Brush vertexTextBrush = Brushes.Black;
        #endregion

        public bool EditRealHeightEnable
        {
            get;
            set;
        }

        public bool PropertyGridVisible
        {
            get { return _editor.pgProperties.Visible; }
            set
            {
                _editor.pgProperties.Visible = value;
                _editor.scContent.Panel2Collapsed = !value;
            }
        }

        public EventHandler ApplyPerformed = null;

        public delegate void VertexHandler(Vertex v);
        public VertexHandler VertexMoving = null;
        public VertexHandler VertexMoved = null;
        public VertexHandler VertexPropertyChanged = null;

        public ConfigurationEditorController(Configuration configuration, ConfigurationEditor editor)
        {
            _config = configuration;
            SetEditor(editor);
        }

        ConfigurationEditorForm _form = null;
        public ConfigurationEditorController(Configuration configuration)
        {
            _config = configuration;
            _form = new ConfigurationEditorForm();
            SetEditor(_form.configEditor);
        }

        public DialogResult ShowForm()
        {
            if (_form == null)
                throw new Exception("Cannot show form in non-windowed mode");

            return _form.ShowDialog();
        }

        private void SetEditor(ConfigurationEditor editor)
        {
            _editor = editor;
            _editor.pbGraph.Paint += new PaintEventHandler(pbGraph_Paint);
            _editor.pbGraph.MouseDown += new MouseEventHandler(pbGraph_MouseDown);
            _editor.pbGraph.MouseMove += new MouseEventHandler(pbGraph_MouseMove);
            _editor.pbGraph.MouseUp += new MouseEventHandler(pbGraph_MouseUp);
            _editor.btnApply.Click += new EventHandler(btnApply_Click);
            _editor.pgProperties.PropertyValueChanged += new PropertyValueChangedEventHandler(pgProperties_PropertyValueChanged);

            //defaults
            EditRealHeightEnable = true;
        }

        void pbGraph_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && _activeVertex != null)
            {
                _activeVertex.X = e.X - _vertexHitX;
                _activeVertex.Y = e.Y - _vertexHitY;
                if (CtrlPressed() && EditRealHeightEnable)
                    _activeVertex.RealHeight = e.Y - _vertexHitY;

                VertexHandler vmh = VertexMoving;
                if (vmh != null)
                    vmh(_activeVertex);
            }
            _editor.pbGraph.Refresh();
        }

        void pbGraph_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && _activeVertex != null)
            {
                VertexHandler vmh = VertexMoved;
                if (vmh != null) 
                    vmh(_activeVertex);
            }
        }

        public static bool CtrlPressed()
        {
            return ((Control.ModifierKeys & Keys.Control) == Keys.Control);
        }
        public static bool AltPressed()
        {
            return ((Control.ModifierKeys & Keys.Alt) == Keys.Alt);
        }

        private double getDistance(int x1, int y1, int x2, int y2)
        {
            return Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
        }
        private double HeronSquare(double a, double b, double c)
        {
            double s = (a + b + c) / 2;
            return Math.Sqrt(s * (s - a) * (s - b) * (s - c));
        }
        protected void pbGraph_MouseDown(object sender, MouseEventArgs e)
        {
            ProcessMouseDown(sender, e);
        }

        protected virtual void ProcessMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            
            _activeVertex = null;
            _activeLink = null;
            _editor.pgProperties.SelectedObject = null;

            foreach (var item in _config.Vertices)
            {
                double r = Math.Sqrt( (e.X - item.X) * (e.X - item.X)
                    + (e.Y - item.Y) * (e.Y - item.Y));
                if (r < VERTEX_RADIUS)
                {
                    _activeVertex = item;
                    _vertexHitX = e.X - item.X;
                    _vertexHitY = e.Y - item.Y;
                    _editor.pgProperties.SelectedObject = _activeVertex.Properties;
                    break;
                }
            }
            if (_activeVertex == null)
            {
                foreach (var link in _config.Links)
                {
                    double a = getDistance(link.Begin.X, link.Begin.Y, link.End.X, link.End.Y);
                    double b = getDistance(link.Begin.X, link.Begin.Y, e.X, e.Y);
                    double c = getDistance(link.End.X, link.End.Y, e.X, e.Y);
                    if (1.1 * a > b + c && 2 * HeronSquare(a, b, c) / a < LINK_HIT_PRECISION)
                    {
                        _activeLink = link;
                        _editor.pgProperties.SelectedObject = _activeLink.Properties;
                        break;
                    }
                }
            }

            _editor.pbGraph.Refresh();
        }

        private void DrawCenteredString(Graphics g, int x, int y, string text, Font f)
        {
            string[] lines = text.Split('\n');
            SizeF[] sizes = new SizeF[lines.Length];
            for (int i = 0; i < lines.Length; i++)
                sizes[i] = g.MeasureString(lines[i], f);
            SizeF fs = g.MeasureString(text, f);
            float lineSpacing = (fs.Height - sizes.Sum(s => s.Height)) / (lines.Length - 1);
            float top = y - fs.Height / 2;
            for (int i = 0; i < lines.Length; i++)
			{
                g.DrawString(lines[i], vertexFont, vertexTextBrush,
                    x - sizes[i].Width / 2, top);

                top += sizes[i].Height + lineSpacing;
			}
        }

        void pbGraph_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);

            if (_config == null) return;

            int maxx = 0;
            int maxy = 0;

            foreach (var vertex in _config.Vertices)
            {
                Rectangle vertexRect = new Rectangle(vertex.X - VERTEX_RADIUS, vertex.Y - VERTEX_RADIUS, 2 * VERTEX_RADIUS, 2 * VERTEX_RADIUS);

                if (vertex.RealHeight != null)
                {
                    int vrh = vertex.RealHeight.Value;
                    e.Graphics.DrawLine(vertexRealHeightPen, vertex.X, vertex.Y, vertex.X, vrh);
                    if (Math.Abs(vrh - vertex.Y) < VERTEX_RADIUS)
                    {
                        e.Graphics.DrawLine(vertexRealHeightPen, vertex.X, vrh, vertex.X + VERTEX_RADIUS, vrh);
                        e.Graphics.FillEllipse(vertexRealHeightBrush, 
                            new Rectangle(vertex.X + VERTEX_RADIUS - VERTEX_REALHEIGHT_RADIUS, 
                                vrh - VERTEX_REALHEIGHT_RADIUS, 
                                2 * VERTEX_REALHEIGHT_RADIUS, 
                                2 * VERTEX_REALHEIGHT_RADIUS));
                    }
                    else
                        e.Graphics.FillEllipse(vertexRealHeightBrush,
                            new Rectangle(vertex.X - VERTEX_REALHEIGHT_RADIUS,
                                vrh - VERTEX_REALHEIGHT_RADIUS,
                                2 * VERTEX_REALHEIGHT_RADIUS,
                                2 * VERTEX_REALHEIGHT_RADIUS));
                    if (maxy < vrh) maxy = vrh;
                }

                if (vertex == _activeVertex)
                    e.Graphics.FillEllipse(activeVertexBrush, vertexRect);
                else
                    e.Graphics.FillEllipse(normalVertexBrush, vertexRect);

                e.Graphics.DrawEllipse(vertexPen, vertexRect);
                if (maxx < vertex.X) maxx = vertex.X;
                if (maxy < vertex.Y) maxy = vertex.Y;
                
                DrawCenteredString(e.Graphics, vertex.X, vertex.Y, vertex.DisplayText, vertexFont);
            }

            foreach (var link in _config.Links)
            {
                int bx = link.Begin.X;
                int by = link.Begin.Y;
                int ex = link.End.X;
                int ey = link.End.Y;
                double angle;
                if (ex == bx)
                {
                    angle = (ey > by) ? Math.PI / 2 : -Math.PI / 2;
                }
                else
                {
                    angle = Math.Atan(((double)ey - by) / (ex - bx));
                    if (ex < bx) angle += Math.PI;
                }
                int pbx = bx + (int)(VERTEX_RADIUS * Math.Cos(angle));
                int pby = by + (int)(VERTEX_RADIUS * Math.Sin(angle));
                angle += Math.PI;
                int pex = ex + (int)(VERTEX_RADIUS * Math.Cos(angle));
                int pey = ey + (int)(VERTEX_RADIUS * Math.Sin(angle));
                Pen linePen = (link == _activeLink) ? linkPenActive : linkPen;
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                e.Graphics.DrawLine(linePen, pbx, pby, pex, pey);
                e.Graphics.DrawLine(linePen, pex, pey, 
                    pex + (int)(ARROW_SIZE * Math.Cos(angle + ARROW_ANGLE)),
                    pey + (int)(ARROW_SIZE * Math.Sin(angle + ARROW_ANGLE)));
                e.Graphics.DrawLine(linePen, pex, pey,
                    pex + (int)(ARROW_SIZE * Math.Cos(angle - ARROW_ANGLE)),
                    pey + (int)(ARROW_SIZE * Math.Sin(angle - ARROW_ANGLE)));
                //e.Graphics.DrawEllipse(linePen, new Rectangle(pex - 10, pey - 10, 20, 20));
            }

            maxx += VERTEX_RADIUS;
            maxy += VERTEX_RADIUS;
            if (_editor.pbGraph.Width != maxx + VERTEX_REALHEIGHT_RADIUS + 3) _editor.pbGraph.Width = maxx + VERTEX_REALHEIGHT_RADIUS + 3;
            if (_editor.pbGraph.Height != maxy + 3) _editor.pbGraph.Height = maxy + 3;
        }

        private void DrawConfig()
        {
            _editor.Refresh();
        }

        public void Refresh()
        {
            DrawConfig();
        }

        void btnApply_Click(object sender, EventArgs e)
        {
            EventHandler eh = ApplyPerformed;
            if (eh != null)
                eh(this, EventArgs.Empty);
        }

        public void UpdatePropertiesGrid()
        {
            _editor.pgProperties.Refresh();
        }

        void pgProperties_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            VertexHandler vmh = VertexPropertyChanged;
            if (vmh != null)
                vmh(_activeVertex);
        }
    }
}
