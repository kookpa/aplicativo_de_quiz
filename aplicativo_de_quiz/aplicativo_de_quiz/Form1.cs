using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace aplicativo_de_quiz
{
    public partial class Form1 : Form
    {
        // Criando uma classe para representar uma pergunta do quiz
        public class Pergunta
        {
            public string Texto { get; set; } // O texto da pergunta
            public string[] Alternativas { get; set; } // As quatro alternativas de resposta
            public int Resposta { get; set; } // O índice da alternativa correta (0 a 3)

            // Um construtor que recebe os parâmetros da pergunta
            public Pergunta(string texto, string[] alternativas, int resposta)
            {
                Texto = texto;
                Alternativas = alternativas;
                Resposta = resposta;
            }
        }
        // Criando uma lista de perguntas para o quiz
        public List<Pergunta> perguntas = new List<Pergunta>()
        {
              new Pergunta("Qual é a capital do Brasil?", new string[] { "Rio de Janeiro", "São Paulo", "Brasília", "Salvador" }, 2),
            new Pergunta("Qual é o maior país do mundo em área?", new string[] { "China", "Estados Unidos", "Rússia", "Canadá" }, 2),
            new Pergunta("Qual é o nome do maior osso do corpo humano?", new string[] { "Fêmur", "Úmero", "Tíbia", "Fíbula" }, 0),
            new Pergunta("Qual é o nome do autor de Dom Quixote?", new string[] { "Miguel de Cervantes", "William Shakespeare", "Jorge Luis Borges", "Gabriel García Márquez" }, 0),
            new Pergunta("Qual é o nome do elemento químico de símbolo Au?", new string[] { "Argônio", "Alumínio", "Ouro", "Urânio" }, 2)
        };
        // Criando variáveis para armazenar o índice da pergunta atual, a pontuação e o número de perguntas
        public int indice = 0;
        public int pontuacao = 0;
        public int total = 5;

        // Criando um objeto da classe Random para gerar números aleatórios
        public Random random = new Random();

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // Embaralhando a lista de perguntas
            perguntas = Embaralhar(perguntas);

            // Exibindo a primeira pergunta
            ExibirPergunta();
        }
        // Um método para embaralhar uma lista usando o algoritmo de Fisher-Yates
        public List<T> Embaralhar<T>(List<T> lista)
        {
            for (int i = lista.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                T temp = lista[i];
                lista[i] = lista[j];
                lista[j] = temp;
            }
            return lista;
        }
        // Um método para exibir a pergunta e atualizar o contador
        public void ExibirPergunta()
        {
            // Verificando se ainda há perguntas na lista
            if (indice < total)
            {
                // Obtendo a pergunta atual
                Pergunta p = perguntas[indice];

                // Atualizando o texto da label e da textbox
                lblPerguntas.Text = p.Texto;
                txbResposta.Text = "";
                // Atualizando o contador
                lblContador.Text = $"Pergunta {indice + 1} de {total}";
            }
            else
            {
                // Encerrando o quiz e mostrando a pontuação
                EncerrarQuiz();
            }
        }
        // Um método para encerrar o quiz e mostrar a pontuação
        public void EncerrarQuiz()
        {
            // Desabilitando os controles
            lblPerguntas.Enabled = false;
            txbResposta.Enabled = false;
            btnVerificar.Enabled = false;

            // Mostrando a pontuação
            MessageBox.Show($"Você acertou {pontuacao} de {total} perguntas.", "Quiz Encerrado");
        }
        // Um evento para verificar a resposta quando o botão for clicado
        private void btnVerificar_Click(object sender, EventArgs e)
        {
            // Obtendo a resposta digitada pelo usuário
            string resposta = txbResposta.Text.Trim().ToLower();

            // Verificando se a resposta está correta
            if (resposta(perguntas[indice].Resposta.ToLower()))
            {
                // Incrementando a pontuação
                pontuacao++;

                // Mostrando uma mensagem de acerto
                MessageBox.Show("Resposta correta!", "Parabéns");
            }
            else 
            {
                // Mostrando uma mensagem de erro
                MessageBox.Show($"Resposta errada. A resposta correta é: {perguntas[indice].Resposta}", "Que pena");
            }
            // Avançando para a próxima pergunta
            indice++;
            ExibirPergunta();
        }
    }
}
