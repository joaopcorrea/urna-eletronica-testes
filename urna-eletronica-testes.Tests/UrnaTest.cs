using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace urna_eletronica_testes.Tests
{
    public class UrnaTest
    {
        [Fact]
        public void ValidarConstrutor_ConstroiCorretamente()
        {
            // Arrange
            var urnaEsperada = new Urna()
            {
                VencedorEleicao = "",
                VotosVencedor = 0,
                Candidatos = new List<Candidato>(),
                EleicaoAtiva = false
            };

            // Act
            var urna = new Urna();

            // Assert
            urna.Should().BeEquivalentTo(urnaEsperada);
        }

        [Fact]
        public void ValidarEleicao_Iniciada_EncerrarEleicao()
        {
            // Arrange
            var urna = new Urna
            {
                EleicaoAtiva = true
            };

            // Act
            urna.IniciarEncerrarEleicao();

            // Assert
            urna.EleicaoAtiva.Should().BeFalse();
        }

        [Fact]
        public void ValidarEleicao_Encerrada_IniciarEleicao()
        {
            // Arrange
            var urna = new Urna
            {
                EleicaoAtiva = false
            };

            // Act
            urna.IniciarEncerrarEleicao();

            // Assert
            urna.EleicaoAtiva.Should().BeTrue();
        }

        [Theory]
        [InlineData("João Pedro")]
        [InlineData("Aline")]
        [InlineData("Leandra")]
        public void ValidarCandidatos_CandidatoNovo_UltimoNaLista(string nome)
        {
            // Arrange
            var urna = new Urna();

            // Act
            urna.CadastrarCandidato(nome);

            // Assert
            urna.Candidatos.Last().Nome.Should().Be(nome);
        }

        [Theory]
        [InlineData("João Pedro")]
        [InlineData("Aline")]
        [InlineData("Leandra")]
        public void ValidarVotacao_CandidatoNaoCadastrado_RetornarFalso(string nome)
        {
            // Arrange
            var urna = new Urna();

            // Act
            var retorno = urna.Votar(nome);

            // Assert
            retorno.Should().BeFalse();
        }

        [Theory]
        [InlineData("João Pedro")]
        [InlineData("Aline")]
        [InlineData("Leandra")]
        public void ValidarVotacao_CandidatoCadastrado_RetornarVerdadeiro(string nome)
        {
            // Arrange
            var urna = new Urna();
            urna.CadastrarCandidato(nome);

            // Act
            var retorno = urna.Votar(nome);

            // Assert
            retorno.Should().BeTrue();
        }

        [Fact]
        public void ValidarResultadoEleicao_RetornarVencedorEVotos()
        {
            // Arrange
            var urna = new Urna();

            var candidatos = new Dictionary<string, int>() { 
                { "João", 382 }, 
                { "Pedro", 98 }, 
                { "Larissa", 0 }, 
                { "Leandra", 500 },
                { "Emerson", 500 },
                { "Bernardo", 12 } 
            };

            var vencedor = candidatos.OrderBy(c => c.Value).ThenBy(c => c.Key).FirstOrDefault();
            var resultado = $"Nome vencedor: {vencedor.Key}. Votos: {vencedor.Value}";

            // Act
            foreach (var c in candidatos)
            {
                urna.CadastrarCandidato(c.Key);

                for (int i = 0; i < c.Value; i++)
                {
                    urna.Votar(c.Key);
                }
            }

            // Assert
            urna.MostrarResultadoEleicao().Should().Be(resultado);
        }
    }
}
