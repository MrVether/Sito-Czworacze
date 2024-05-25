using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Sito_Czworacze
{
    public partial class Sito_Czworacze : Form
    {
        private TextBox textBoxRange;
        private Button btnCalculate;
        private TextBox textBoxPrimes;
        private TextBox textBoxTwinPairs;
        private TextBox textBoxQuartets;
        private Label labelRange;
        private Label labelPrimes;
        private Label labelTwinPairs;
        private Label labelQuartets;

        public Sito_Czworacze()
        {
            InitializeComponent();
            InitializeCustomComponents();
            this.BackColor = Color.LightGray;
            this.Text = "Sito Czworacze - Liczby Pierwsze i ich Grupy";
            this.Size = new Size(500, 400); 
        }

        private void InitializeCustomComponents()
        {
            labelRange = new Label
            {
                Location = new Point(20, 20),
                Size = new Size(120, 20),
                Text = "Podaj zakres:",
                Font = new Font("Consolas", 10, FontStyle.Bold)
            };

            textBoxRange = new TextBox
            {
                Location = new Point(150, 20),
                Size = new Size(150, 20),
                Font = new Font("Consolas", 10, FontStyle.Bold)
            };

            btnCalculate = new Button
            {
                Location = new Point(320, 20),
                Size = new Size(50, 23),
                Text = "OK",
                BackColor = Color.LightBlue,
                Font = new Font("Consolas", 10, FontStyle.Bold)
            };
            btnCalculate.Click += new EventHandler(btnCalculate_Click);

            labelPrimes = new Label
            {
                Location = new Point(20, 50),
                Size = new Size(150, 20),
                Text = "Liczby pierwsze:",
                Font = new Font("Consolas", 10, FontStyle.Bold)
            };

            textBoxPrimes = new TextBox
            {
                Location = new Point(20, 70),
                Size = new Size(450, 60),
                Multiline = true,
                ReadOnly = true,
                BackColor = Color.White,
                Font = new Font("Consolas", 9),
                BorderStyle = BorderStyle.FixedSingle
            };

            labelTwinPairs = new Label
            {
                Location = new Point(20, 140),
                Size = new Size(150, 20),
                Text = "Blizniacze pary:",
                Font = new Font("Consolas", 10, FontStyle.Bold)
            };

            textBoxTwinPairs = new TextBox
            {
                Location = new Point(20, 160),
                Size = new Size(450, 60),
                Multiline = true,
                ReadOnly = true,
                BackColor = Color.White,
                Font = new Font("Consolas", 9),
                BorderStyle = BorderStyle.FixedSingle
            };

            labelQuartets = new Label
            {
                Location = new Point(20, 230),
                Size = new Size(150, 20),
                Text = "Czworki:",
                Font = new Font("Consolas", 10, FontStyle.Bold)
            };

            textBoxQuartets = new TextBox
            {
                Location = new Point(20, 250),
                Size = new Size(450, 60),
                Multiline = true,
                ReadOnly = true,
                BackColor = Color.White,
                Font = new Font("Consolas", 9),
                BorderStyle = BorderStyle.FixedSingle
            };

            this.Controls.Add(labelRange);
            this.Controls.Add(textBoxRange);
            this.Controls.Add(btnCalculate);
            this.Controls.Add(labelPrimes);
            this.Controls.Add(textBoxPrimes);
            this.Controls.Add(labelTwinPairs);
            this.Controls.Add(textBoxTwinPairs);
            this.Controls.Add(labelQuartets);
            this.Controls.Add(textBoxQuartets);
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            textBoxPrimes.Text = "";
            textBoxTwinPairs.Text = "";
            textBoxQuartets.Text = "";
            int n;
            if (int.TryParse(textBoxRange.Text, out n) && n > 2)
            {
                bool[] sieve = new bool[n + 1];
                List<int> primes = new List<int>();
                for (int i = 3; i * i <= n; i += 2)
                {
                    if (!sieve[i])
                    {
                        for (int j = i * i; j <= n; j += i)
                            sieve[j] = true;
                    }
                }

                List<string> primeResults = new List<string>();
                for (int i = 3; i <= n; i += 2)
                {
                    if (!sieve[i])
                    {
                        primes.Add(i);
                        primeResults.Add(i.ToString());
                    }
                }

                textBoxPrimes.Text = $"Liczby pierwsze >= {n}:\n" + String.Join(", ", primeResults);

                List<string> twinResults = new List<string>();
                for (int i = 0; i < primes.Count - 1; i++)
                {
                    if (primes[i + 1] - primes[i] == 2)
                    {
                        twinResults.Add($"({primes[i]}, {primes[i + 1]})");
                    }
                }

                textBoxTwinPairs.Text = "Blizniacze pary:\n" + String.Join(", ", twinResults);

                List<string> quartetResults = new List<string>();
                for (int i = 0; i < primes.Count - 3; i++)
                {
                    if (primes[i + 1] - primes[i] == 2 && primes[i + 2] - primes[i + 1] == 4 && primes[i + 3] - primes[i + 2] == 2)
                    {
                        quartetResults.Add($"({primes[i]}, {primes[i + 1]}, {primes[i + 2]}, {primes[i + 3]})");
                    }
                }

                textBoxQuartets.Text = "Czworki:\n" + String.Join(", ", quartetResults);
            }
            else
            {
                MessageBox.Show("Please enter a valid number greater than 2.");
            }
        }
    }
}
