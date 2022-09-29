using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace urna_eletronica_testes.Tests
{
    public class CandidatoTest
    {
        [Theory]
        [InlineData("João")]
        [InlineData("Pedro")]
        [InlineData("Aline")]
        [InlineData("Leandra")]
        public void ValidarQuantidadeVotos_AposCadastroCandidato_VotosIgualAZero(string nome)
        {
            // Arrange 
            // Act
            var candidato = new Candidato(nome);

            // Assert
            candidato.RetornarVotos().Should().Be(0);
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 2)]
        [InlineData(9, 10)]
        [InlineData(10, 11)]
        [InlineData(531, 532)]
        [InlineData(999999, 1000000)]
        public void ValidarQuantidadeVotos_AposNovoVoto_VotosMaisUm(int votosAntes, int votosEsperados)
        {
            // Arrange
            var candidato = new Candidato("Lula");
            candidato.Votos = votosAntes;
            
            // Act
            candidato.AdicionarVoto();

            // Assert
            candidato.RetornarVotos().Should().Be(votosEsperados);
        }

        [Theory]
        [InlineData("João Pedro")]
        [InlineData("Aline")]
        [InlineData("Leandra")]
        public void ValidarNomeCandidato_AposCadastro_NomeCorreto(string nome)
        {
            // Arrange
            // Act
            var candidado = new Candidato(nome);

            // Assert
            candidado.Nome.Should().Be(nome);
        }
    }
}
