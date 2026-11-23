using System;
using System.Drawing;
using System.Windows.Forms;

using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ProjLeds
{
    public partial class Form1 : Form
    {
        private Leds leds = new Leds();
        private PictureBox[] picLeds = new PictureBox[8];
        private Label lblByte, lblHex, lblBits;
        private Image imgAceso;
        private Image imgApagado;

        public Form1()
        {
            InitializeComponent();
            CarregarImagens();
            MontarInterface();
            AtualizarTela();
        }

        private void CarregarImagens()
        {
            string pasta = Path.Combine(Application.StartupPath, "Imagens");
            imgAceso = Image.FromFile(Path.Combine(pasta, "led_aceso.png"));
            imgApagado = Image.FromFile(Path.Combine(pasta, "led_apagado.png"));
        }

        private void MontarInterface()
        {
            Text = "ProjLeds";
            ClientSize = new Size(560, 270);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            // Da esquerda para a direita: LED 8 ... LED 1
            // (igual ? ordem da string de bits)
            for (int i = 0; i < 8; i++)
            {
                int led = 8 - i;
                int x = 20 + i * 65;

                PictureBox pic = new PictureBox();
                pic.Location = new Point(x, 20);
                pic.Size = new Size(60, 60);
                pic.SizeMode = PictureBoxSizeMode.Zoom;
                Controls.Add(pic);
                picLeds[led - 1] = pic;

                Button btn = new Button();
                btn.Location = new Point(x, 90);
                btn.Size = new Size(60, 35);
                btn.Text = led.ToString();
                btn.Tag = led;
                btn.Click += Botao_Click;
                Controls.Add(btn);
            }

            lblByte = CriarLabel(150);
            lblHex = CriarLabel(185);
            lblBits = CriarLabel(220);
        }

        private Label CriarLabel(int y)
        {
            Label lbl = new Label();
            lbl.Location = new Point(20, y);
            lbl.Size = new Size(520, 30);
            lbl.Font = new Font("Consolas", 14);
            Controls.Add(lbl);
            return lbl;
        }

        private void Botao_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int led = (int)btn.Tag;

            if (leds.getStatus(led))
                leds.apagar(led);
            else
                leds.acender(led);

            AtualizarTela();
        }

        private void AtualizarTela()
        {
            for (int led = 1; led <= 8; led++)
            {
                picLeds[led - 1].Image = leds.getStatus(led) ? imgAceso : imgApagado;
            }

            byte valor = leds.getStatus();
            lblByte.Text = "Byte: " + valor;
            lblHex.Text = "Hex:  0x" + valor.ToString("X2");
            lblBits.Text = "Bits: " + Convert.ToString(valor, 2).PadLeft(8, '0');
        }
    }
}