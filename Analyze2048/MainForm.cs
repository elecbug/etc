namespace Analyze2048
{
    public partial class MainForm : Form
    {
        public const int EdgeCount = 4;
        public const int EdgeSize = 100;
        public BlockBox?[,] boxes = new BlockBox[EdgeCount, EdgeCount];

        public int Grade { get; set; }

        public enum Vector { Up, Down, Left, Right }

        public MainForm()
        {
            InitializeComponent();

            //for (int i = 0; i < EdgeCount; i++)
            //{
            //    for (int j = 0; j < EdgeCount; j++)
            //    {
            //        this.boxes[i, j] = new BlockBox()
            //        {
            //            Parent = this,
            //            Visible = true,
            //            Location = new Point(i * EdgeSize, j * EdgeSize),
            //            Size = new Size(EdgeSize, EdgeSize),
            //        };
            //    }
            //}

            Click += (s, e) =>
            {
                MoveBlock(Vector.Right);
                CreateRandomBlock();

                this.Text = $"{this.Grade}";
            };
        }

        public void MoveBlock(Vector v)
        {
            switch (v)
            {
                case Vector.Up:
                    {
                        for (int y = 0; y < EdgeCount; y++)
                        {
                            for (int x = 0; x < EdgeCount; x++)
                            {
                                if (this.boxes[x, y] != null)
                                {
                                    int c = 1;

                                    while (y - c >= 0)
                                    {
                                        if (this.boxes[x, y - c] == null)
                                        {
                                            this.boxes[x, y - c] = this.boxes[x, y - (c - 1)];
                                            this.boxes[x, y - (c - 1)] = null;

                                            this.boxes[x, y - c]!.Location = new Point(x * EdgeSize, (y - c) * EdgeSize);
                                        }
                                        else if (this.boxes[x, y - c]!.Level == this.boxes[x, y - (c - 1)]!.Level)
                                        {
                                            this.boxes[x, y - c]!.Level += 1;
                                            this.boxes[x, y - (c - 1)]!.Visible = false;
                                            this.boxes[x, y - (c - 1)]!.Dispose();
                                            this.boxes[x, y - (c - 1)] = null;

                                            this.Grade += (int)Math.Pow(2, this.boxes[x, y - c]!.Level);

                                            break;
                                        }

                                        c++;
                                    }
                                }
                            }
                        }
                    }
                    return;
                case Vector.Down:
                    {
                        for (int y = EdgeCount - 1; y >= 0; y--)
                        {
                            for (int x = 0; x < EdgeCount; x++)
                            {
                                if (this.boxes[x, y] != null)
                                {
                                    int c = 1;

                                    while (y + c < EdgeCount)
                                    {
                                        if (this.boxes[x, y + c] == null)
                                        {
                                            this.boxes[x, y + c] = this.boxes[x, y + (c - 1)];
                                            this.boxes[x, y + (c - 1)] = null;

                                            this.boxes[x, y + c]!.Location = new Point(x * EdgeSize, (y + c) * EdgeSize);
                                        }
                                        else if (this.boxes[x, y + c]!.Level == this.boxes[x, y + (c - 1)]!.Level)
                                        {
                                            this.boxes[x, y + c]!.Level += 1;
                                            this.boxes[x, y + (c - 1)]!.Visible = false;
                                            this.boxes[x, y + (c - 1)]!.Dispose();
                                            this.boxes[x, y + (c - 1)] = null;

                                            this.Grade += (int)Math.Pow(2, this.boxes[x, y + c]!.Level);

                                            break;
                                        }

                                        c++;
                                    }
                                }
                            }
                        }
                    }
                    return;
                case Vector.Left:
                    {
                        for (int x = 0; x < EdgeCount; x++)
                        {
                            for (int y = 0; y < EdgeCount; y++)
                            {
                                if (this.boxes[x, y] != null)
                                {
                                    int c = 1;

                                    while (x - c >= 0)
                                    {
                                        if (this.boxes[x - c, y] == null)
                                        {
                                            this.boxes[x - c, y] = this.boxes[x - (c - 1), y];
                                            this.boxes[x - (c - 1), y] = null;

                                            this.boxes[x - c, y]!.Location = new Point((x - c) * EdgeSize, y * EdgeSize);
                                        }
                                        else if (this.boxes[x - c, y]!.Level == this.boxes[x - (c - 1), y]!.Level)
                                        {
                                            this.boxes[x - c, y]!.Level += 1;
                                            this.boxes[x - (c - 1), y]!.Visible = false;
                                            this.boxes[x - (c - 1), y]!.Dispose();
                                            this.boxes[x - (c - 1), y] = null;

                                            this.Grade += (int)Math.Pow(2, this.boxes[x - c, y]!.Level);

                                            break;
                                        }

                                        c++;
                                    }
                                }
                            }
                        }
                    }
                    return;
                case Vector.Right:
                    {
                        for (int x = EdgeCount - 1; x >= 0; x--)
                        {
                            for (int y = 0; y < EdgeCount; y++)
                            {
                                if (this.boxes[x, y] != null)
                                {
                                    int c = 1;

                                    while (x + c < EdgeCount)
                                    {
                                        if (this.boxes[x + c, y] == null)
                                        {
                                            this.boxes[x + c, y] = this.boxes[x + (c - 1), y];
                                            this.boxes[x + (c - 1), y] = null;

                                            this.boxes[x + c, y]!.Location = new Point((x + c) * EdgeSize, y * EdgeSize);
                                        }
                                        else if (this.boxes[x + c, y]!.Level == this.boxes[x + (c - 1), y]!.Level)
                                        {
                                            this.boxes[x + c, y]!.Level += 1;
                                            this.boxes[x + (c - 1), y]!.Visible = false;
                                            this.boxes[x + (c - 1), y]!.Dispose();
                                            this.boxes[x + (c - 1), y] = null;

                                            this.Grade += (int)Math.Pow(2, this.boxes[x + c, y]!.Level);

                                            break;
                                        }

                                        c++;
                                    }
                                }
                            }
                        }
                    }
                    return;
            }
        }

        public void CreateRandomBlock()
        {
            List<Point> nulls = new List<Point>();

            for (int w = 0; w < EdgeCount; w++)
            {
                for (int h = 0; h < EdgeCount; h++)
                {
                    if (this.boxes[w, h] == null)
                    {
                        nulls.Add(new Point(w, h));
                    }
                }
            }

            if (nulls.Count == 0) 
            {
                GameOver();
            }

            Shuffle<Point>(nulls);

            this.boxes[nulls.Last().X, nulls.Last().Y] = new BlockBox()
            {
                Parent = this,
                Visible = true,
                Location = new Point(nulls.Last().X * EdgeSize, nulls.Last().Y * EdgeSize),
                Size = new Size(EdgeSize, EdgeSize),
            };
        }

        private void GameOver()
        {
            MessageBox.Show("Game Over", "Game Over", buttons: MessageBoxButtons.OK);

            for (int w = 0; w < EdgeCount; w++)
            {
                for (int h = 0; h < EdgeCount; h++)
                {
                    if (this.boxes[w, h] != null)
                    {
                        this.boxes[w, h]!.Visible = false;
                        this.boxes[w, h]!.Dispose();
                        this.boxes[w, h] = null;
                    }
                }
            }
        }

        private void Shuffle<T>(IList<T> list)
        {
            int n = list.Count;

            while (n > 1)
            {
                n--;
                int k = new Random((int)DateTime.Now.Ticks).Next(n + 1);

                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}