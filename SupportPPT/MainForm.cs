using System.Diagnostics;
using PPT = Microsoft.Office.Interop.PowerPoint;

namespace SupportPPT
{
    public partial class MainForm : Form
    {
        private PPT.Application application;
        private PPT.Presentation? presentation;

        private Stack<PPT.Slides> slides;

        public MainForm()
        {
            InitializeComponent();

            this.slides = new Stack<PPT.Slides>();
        }

        private void MainFormLoad(object sender, EventArgs e)
        {
            this.application = new PPT.Application();
            this.application.PresentationClose += PresentationClose;
        }

        private void PresentationClose(PPT.Presentation Pres)
        {
            this.presentation = null;
        }

        private void MainFormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.presentation != null)
            {
                DialogResult result = MessageBox.Show("Close the Presentation?", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                
                if (result == DialogResult.Yes)
                {
                    this.presentation.Close();
                    this.application.Quit();
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        private void SaveClick(object sender, EventArgs e)
        {
            if (this.presentation != null)
            {
                this.presentation.Save();
            }
            else
            {
                DontOpen();
            }
        }

        private void OpenClick(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Filter = "PPT | *.pptx",
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.presentation = this.application.Presentations.Open(dialog.FileName);
            }
        }

        private void DontOpen()
        {
            MessageBox.Show("Don't open ppt file now!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void SortAlignClick(object sender, EventArgs e)
        {
            if (this.presentation != null)
            {
                this.slides.Push(this.presentation.Slides);

                for (int i = 1; i <= this.presentation.Slides.Count; i++)
                {
                    this.presentation.Slides[i].Layout = this.presentation.Slides[i].Layout;
                }
            }
            else
            {
                DontOpen();
            }
        }

        //private void FontChangeClick(object sender, EventArgs e)
        //{
        //    if (this.presentation != null)
        //    {
        //        InputDialog dialog = new InputDialog(2);

        //        if (dialog.ShowDialog() != DialogResult.OK)
        //        {
        //            while (true)
        //            {
        //                try
        //                {
        //                    this.presentation.Fonts.Replace(this.presentation.Fonts.ToString(), dialog.TextBoxes[1].Text);

        //                    break;
        //                }
        //                catch (Exception ex)
        //                {
        //                    Debug.WriteLine(ex);

        //                    continue;
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        DontOpen();
        //    }
        //}
    }

    class InputDialog : Form
    {
        public int RowSize { get; private set; } = 1;
        public List<TextBox> TextBoxes { get; private set; }

        public InputDialog(int rowSize)
        {
            this.RowSize = rowSize;
            this.TextBoxes = new List<TextBox>();

            this.ClientSize = new Size(310, 35 * (rowSize + 1));

            for (int i = 0; i < this.RowSize; i++)
            {
                this.TextBoxes.Add(new TextBox()
                {
                    Parent = this,
                    Visible = true,
                    Location = new Point(5, 5 + (i * 35)),
                    Size = new Size(300, 30),
                });
            }

            Button ok = new Button()
            {
                Parent = this,
                Visible = true,
                Location = new Point(5, 5 + (rowSize * 35)),
                Size = new Size(300, 30),
                Text = "OK"
            };
            ok.Click += OkClick;
        }

        private void OkClick(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}