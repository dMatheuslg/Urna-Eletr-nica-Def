using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Urna_eletronica_def
{
    public partial class Form1 : Form
    {
        private Dictionary<string, Candidato> _dicCanditado;
        private Dictionary<string, int> _votos;

        public Form1()
        {
            InitializeComponent();
            _dicCanditado = new Dictionary<string, Candidato>();
            _votos = new Dictionary<string, int>();

            _dicCanditado.Add("28", new Candidato() { Id = 28, Nome = "Michael Jackson", Partido = "Lord", Foto = Properties.Resources.mike });
            _dicCanditado.Add("99", new Candidato() { Id = 99, Nome = "P.Diddy", Partido = "R.A.P", Foto = Properties.Resources.didi });

            _votos["28"] = 0;
            _votos["99"] = 0;
        }

        private void btn1_Click(object sender, EventArgs e) => RegistrarDigito("1");
        private void btn2_Click(object sender, EventArgs e) => RegistrarDigito("2");
        private void btn3_Click(object sender, EventArgs e) => RegistrarDigito("3");
        private void btn4_Click(object sender, EventArgs e) => RegistrarDigito("4");
        private void btn5_Click(object sender, EventArgs e) => RegistrarDigito("5");
        private void btn6_Click(object sender, EventArgs e) => RegistrarDigito("6");
        private void btn7_Click(object sender, EventArgs e) => RegistrarDigito("7");
        private void btn8_Click(object sender, EventArgs e) => RegistrarDigito("8");
        private void btn9_Click(object sender, EventArgs e) => RegistrarDigito("9");
        private void btn0_Click(object sender, EventArgs e) => RegistrarDigito("0");

        private void btnbranco_Click(object sender, EventArgs e)
        {
            panelfim.Visible = true;
            Limpar();
        }

        private void btncorrige_Click(object sender, EventArgs e)
        {
            Limpar();
        }

        private void btnconfirmar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtgovernador1.Text))
            {
                MessageBox.Show("Favor informar o candidato.");
                return;
            }

            string candidatoId = txtgovernador1.Text + txtgovernador2.Text;
            if (_dicCanditado.ContainsKey(candidatoId))
            {
                _votos[candidatoId]++;
            }
            else
            {
                MessageBox.Show("Candidato não encontrado!");
            }

            panelfim.Visible = true;
            Limpar();
            btnmostrarvencedor.Visible = true;
        }

        private void RegistrarDigito(string digito)
        {
            if (string.IsNullOrEmpty(txtgovernador1.Text))
            {
                txtgovernador1.Text = digito;
            }
            else
            {
                txtgovernador2.Text = digito;
                PreencherCandidato(txtgovernador1.Text, txtgovernador2.Text);
            }
        }

        private void PreencherCandidato(string d1, string d2)
        {
            string candidatoId = d1 + d2;
            if (_dicCanditado.ContainsKey(candidatoId))
            {
                label1.Text = _dicCanditado[candidatoId].Nome;
                label2.Text = _dicCanditado[candidatoId].Partido;
                pictureBoxCandidato.Image = _dicCanditado[candidatoId].Foto;
            }
            else
            {
                MessageBox.Show("Candidato não encontrado!");
            }
        }

        private void Limpar()
        {
            txtgovernador1.Text = "";
            txtgovernador2.Text = "";
            label1.Text = String.Empty;
            label2.Text = String.Empty;
            pictureBoxCandidato.Image = null;
        }

        private void btnvcand_Click(object sender, EventArgs e)
        {
            panelcand.Visible = true;
            btnfechar.Visible = true;
        }

        private void btnfechar_Click(object sender, EventArgs e)
        {
            panelcand.Visible = false;
            btnfechar.Visible = false;
        }

        private void btnmostrarvencedor_Click(object sender, EventArgs e)
        {
            MostrarVencedor();
            DesativarBotoes();
        }

        private void MostrarVencedor()
        {
            var votosOrdenados = _votos.OrderByDescending(v => v.Value).ToList();

            // Verifica se há empate
            if (votosOrdenados[0].Value == votosOrdenados[1].Value)
            {
                DialogResult resultado = MessageBox.Show("Empate, ir para o segundo turno?", "Empate", MessageBoxButtons.OKCancel);
                if (resultado == DialogResult.OK)
                {
                    ResetarVotos();
                    AtivarBotoes();
                    Limpar();
                }
            }
            else
            {
                // Exibe o vencedor
                string candidatoId = votosOrdenados[0].Key;
                int votosVencedor = votosOrdenados[0].Value;

                if (_dicCanditado.ContainsKey(candidatoId))
                {
                    Candidato candidatoVencedor = _dicCanditado[candidatoId];

                    labelnvencedor.Text = candidatoVencedor.Id.ToString();
                    labelnomevencedor.Text = candidatoVencedor.Nome;
                    labelpvencedor.Text = candidatoVencedor.Partido;
                    picvencedor.Image = candidatoVencedor.Foto;
                    labelvotos.Text = $"Votos recebidos: {votosVencedor}";

                    panelven.Visible = true;
                }
                else
                {
                    MessageBox.Show("Nenhum vencedor encontrado.");
                }
            }
        }

        private void ResetarVotos()
        {
            foreach (var key in _votos.Keys.ToList())
            {
                _votos[key] = 0;
            }
        }

        private void DesativarBotoes()
        {
            btn1.Enabled = false;
            btn2.Enabled = false;
            btn3.Enabled = false;
            btn4.Enabled = false;
            btn5.Enabled = false;
            btn6.Enabled = false;
            btn7.Enabled = false;
            btn8.Enabled = false;
            btn9.Enabled = false;
            btn0.Enabled = false;
            btnbranco.Enabled = false;
            btncorrige.Enabled = false;
            btnconfirmar.Enabled = false;
            btnvcand.Enabled = false;
            btnmostrarvencedor.Enabled = false;
        }

        private void AtivarBotoes()
        {
            btn1.Enabled = true;
            btn2.Enabled = true;
            btn3.Enabled = true;
            btn4.Enabled = true;
            btn5.Enabled = true;
            btn6.Enabled = true;
            btn7.Enabled = true;
            btn8.Enabled = true;
            btn9.Enabled = true;
            btn0.Enabled = true;
            btnbranco.Enabled = true;
            btncorrige.Enabled = true;
            btnconfirmar.Enabled = true;
            btnvcand.Enabled = true;
            btnmostrarvencedor.Enabled = true;
        }

        private void btnvotarnovamente_Click(object sender, EventArgs e)
        {
            panelfim.Visible = false;
            Limpar();
            AtivarBotoes();
        }

        public class Candidato
        {
            public int Id { get; set; }
            public string Nome { get; set; }
            public string Partido { get; set; }
            public Image Foto { get; set; }
        }
    }
}
