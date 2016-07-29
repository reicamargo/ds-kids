using System;
using System.Collections.Generic;

using DS.Kids.Model;

namespace DS.Kids.Testes.Services.__Fakes.Repositories
{
    //TODO: REVER A FORMA DE CRIAÇÃO DE REPOSITÓRIOS FAKES
    public class Database
    {
        public readonly List<Alimento> alimentos;
        public readonly List<Crianca> criancas;
        public readonly List<Crescimento> crescimentos;
        public readonly List<LoginSocial> logins_Sociais;
        public readonly List<Responsavel> responsaveis;
        public readonly List<Token> tokens;

        public readonly List<Refeicao> cafe_Da_Manha;
        public readonly List<Refeicao> lanche_Da_Manha;
        public readonly List<Refeicao> almoco;
        public readonly List<Refeicao> lanche_Da_Tarde;
        public readonly List<Refeicao> jantar;
        public readonly List<Refeicao> lanche_Da_Noite;

        public readonly List<RefeicaoGrupo> refeicoes_Grupo = new List<RefeicaoGrupo>();
        public readonly List<RefeicaoDiario> refeicoes_Diario = new List<RefeicaoDiario>
                                                                                { 
            new RefeicaoDiario
                {
                    IdCrianca = 1,
                    DataCriacao = DateTime.Now,
                    IdRefeicao = 1,
                    IdTipoRefeicao = (int)TipoRefeicao.Almoco,
                    TipoRefeicao = TipoRefeicao.Almoco,
                    RefeicoesGrupos = new List<RefeicaoGrupo>
                                    {
                                    new RefeicaoGrupo()
                                    }
                }
        };

        public readonly Alimento alimento1;
        public readonly Alimento alimento2;
        public readonly Alimento alimento3;
        public readonly Alimento alimento4;
        public readonly Alimento alimento5;

        public Database()
        {
            if (responsaveis != null)
                return;

            alimentos = new List<Alimento>();
            responsaveis = new List<Responsavel>();
            tokens = new List<Token>();
            logins_Sociais = new List<LoginSocial>();
            criancas = new List<Crianca>();
            crescimentos = new List<Crescimento>();
            cafe_Da_Manha = new List<Refeicao>();
            lanche_Da_Manha = new List<Refeicao>();
            almoco = new List<Refeicao>();
            lanche_Da_Tarde = new List<Refeicao>();
            jantar = new List<Refeicao>();
            lanche_Da_Noite = new List<Refeicao>();


            var senha = "ba3253876aed6bc22d4a6ff53d8406c6ad864195ed144ab5c87621b6c233b548baeae6956df346ec8c17f5ea10f35ee3cbc514797ed7ddd3145464e2a0bab413";
            /*Cria os Responsáveis*/
            var responsavel1 = new Responsavel { IdResponsavel = 1, Nome = "José", Senha = senha, Telefone = "11987654321", Email = "jose@email.com.br" };
            var responsavel2 = new Responsavel { IdResponsavel = 2, Nome = "Maria", Senha = senha, Telefone = "11987654322", Email = "maria@email.com.br" };
            var responsavel3 = new Responsavel { IdResponsavel = 3, Nome = "João", Senha = senha, Telefone = "11987654323", Email = "joao@email.com.br" };
            var responsavel4 = new Responsavel { IdResponsavel = 4, Nome = "Cleide", Senha = senha, Telefone = "11987654324", Email = "cleide@email.com.br" };
            var responsavel5 = new Responsavel { IdResponsavel = 5, Nome = "Roberval", Senha = senha, Telefone = "11987654325", Email = "roberval@email.com.br" };

            /*Cria Tokens com um Responsável*/
            var token1 = new Token { ResponsavelId = responsavel1.IdResponsavel, Responsavel = responsavel1, Valor = "1234567890" };
            var token2 = new Token { ResponsavelId = responsavel2.IdResponsavel, Responsavel = responsavel2, Valor = "1234567891" };
            var token3 = new Token { ResponsavelId = responsavel3.IdResponsavel, Responsavel = responsavel3, Valor = "1234567892" };
            var token4 = new Token { ResponsavelId = responsavel4.IdResponsavel, Responsavel = responsavel4, Valor = "1234567893" };
            var token5 = new Token { ResponsavelId = responsavel5.IdResponsavel, Responsavel = responsavel5, Valor = "1234567894" };

            /*Associa Token ao Responsavel*/
            responsavel1.Token = token1;
            responsavel2.Token = token2;
            responsavel3.Token = token3;
            responsavel4.Token = token4;
            responsavel5.Token = token5;

            /*Cria Login Social com um Responsável*/
            var loginSocial1 = new LoginSocial { IdResponsavel = responsavel1.IdResponsavel, Responsavel = responsavel1, Chave = "123", Email = responsavel1.Email, Nome = responsavel1.Nome, RedeSocial = RedesSociais.Facebook };
            var loginSocial2 = new LoginSocial { IdResponsavel = responsavel2.IdResponsavel, Responsavel = responsavel2, Chave = "456", Email = responsavel2.Email, Nome = responsavel2.Nome, RedeSocial = RedesSociais.Facebook };
            var loginSocial3 = new LoginSocial { IdResponsavel = responsavel3.IdResponsavel, Responsavel = responsavel3, Chave = "789", Email = responsavel3.Email, Nome = responsavel3.Nome, RedeSocial = RedesSociais.Facebook };

            /*Associa Login Social ao Responsavel*/
            responsavel1.LoginSocial = loginSocial1;
            responsavel2.LoginSocial = loginSocial2;
            responsavel3.LoginSocial = loginSocial3;

            /*Cria as Crianças*/
            var crianca1 = new Crianca { IdCrianca = 1, IdResponsavel = responsavel1.IdResponsavel, Responsavel = responsavel1, DataNascimento = DateTime.Now.AddYears(-2), Nome = "Pedro"     , Sexo = "M", AlturaInicial = 1.20m, PesoInicial = 14m };
            var crianca2 = new Crianca { IdCrianca = 2, IdResponsavel = responsavel1.IdResponsavel, Responsavel = responsavel1, DataNascimento = DateTime.Now.AddYears(-8), Nome = "Otávio"    , Sexo = "M", AlturaInicial = 1.50m, PesoInicial = 30m };
            var crianca3 = new Crianca { IdCrianca = 3, IdResponsavel = responsavel2.IdResponsavel, Responsavel = responsavel2, DataNascimento = DateTime.Now.AddYears(-4), Nome = "Joãozinho" , Sexo = "M", AlturaInicial = 1.10m, PesoInicial = 20m };
            var crianca4 = new Crianca { IdCrianca = 4, IdResponsavel = responsavel2.IdResponsavel, Responsavel = responsavel2, DataNascimento = DateTime.Now.AddYears(-6), Nome = "Mariazinha", Sexo = "F", AlturaInicial = 1.40m, PesoInicial = 25m };
            var crianca5 = new Crianca { IdCrianca = 5, IdResponsavel = responsavel3.IdResponsavel, Responsavel = responsavel3, DataNascimento = DateTime.Now.AddYears(-3), Nome = "Eduardo"   , Sexo = "M", AlturaInicial = 1.20m, PesoInicial = 22m };
            var crianca6 = new Crianca { IdCrianca = 6, IdResponsavel = responsavel3.IdResponsavel, Responsavel = responsavel3, DataNascimento = DateTime.Now.AddYears(-5), Nome = "Angelo"    , Sexo = "M", AlturaInicial = 1.30m, PesoInicial = 24m };
            var crianca7 = new Crianca { IdCrianca = 7, IdResponsavel = responsavel4.IdResponsavel, Responsavel = responsavel4, DataNascimento = DateTime.Now.AddYears(-3), Nome = "Eduardo"   , Sexo = "M", AlturaInicial = 1.20m, PesoInicial = 22m };
            var crianca8 = new Crianca { IdCrianca = 8, IdResponsavel = responsavel4.IdResponsavel, Responsavel = responsavel4, DataNascimento = DateTime.Now.AddYears(-5), Nome = "Angelo"    , Sexo = "M", AlturaInicial = 1.30m, PesoInicial = 24m };
            var crianca9 = new Crianca { IdCrianca = 9, IdResponsavel = responsavel5.IdResponsavel, Responsavel = responsavel5, DataNascimento = DateTime.Now.AddYears(-3), Nome = "Eduardo"   , Sexo = "M", AlturaInicial = 1.20m, PesoInicial = 22m };
            var crianca10 = new Crianca { IdCrianca = 10, IdResponsavel = responsavel5.IdResponsavel, Responsavel = responsavel5, DataNascimento = DateTime.Now.AddYears(-5), Nome = "Angelo"  , Sexo = "M", AlturaInicial = 1.30m, PesoInicial = 24m };

            /*Adiciona as Crianças aos responsáveis*/
            responsavel1.Criancas.Add(crianca1);
            responsavel1.Criancas.Add(crianca2);
            responsavel2.Criancas.Add(crianca3);
            responsavel2.Criancas.Add(crianca4);
            responsavel3.Criancas.Add(crianca5);
            responsavel3.Criancas.Add(crianca6);
            responsavel4.Criancas.Add(crianca7);
            responsavel4.Criancas.Add(crianca8);
            responsavel5.Criancas.Add(crianca9);
            responsavel5.Criancas.Add(crianca10);

            /*Cria Alimentos*/
            alimento1 = new Alimento { IdAlimento = 1, IdGrupo = 1, Nome = "Pera" };
            alimento2 = new Alimento { IdAlimento = 2, IdGrupo = 2, Nome = "Uva" };
            alimento3 = new Alimento { IdAlimento = 3, IdGrupo = 3, Nome = "Maçã" };
            alimento4 = new Alimento { IdAlimento = 4, IdGrupo = 4, Nome = "Salada Mista" };
            alimento5 = new Alimento { IdAlimento = 5, IdGrupo = 1, Nome = "Lata de leite condensado" };

            /*Cria Crescimentos*/
            var crescimento1 = new Crescimento { IdCrescimento = 1, IdCrianca = crianca1.IdCrianca, Crianca = crianca1, Altura = crianca1.AlturaInicial + 1, MesesDeIdade = crianca1.DataNascimento.GetTotalMonths(), Peso = crianca1.PesoInicial + 1 };
            var crescimento2 = new Crescimento { IdCrescimento = 2, IdCrianca = crianca1.IdCrianca, Crianca = crianca1, Altura = crianca1.AlturaInicial + 2, MesesDeIdade = crianca1.DataNascimento.GetTotalMonths(), Peso = crianca1.PesoInicial + 2 };
            var crescimento3 = new Crescimento { IdCrescimento = 3, IdCrianca = crianca2.IdCrianca, Crianca = crianca2, Altura = crianca2.AlturaInicial + 1, MesesDeIdade = crianca2.DataNascimento.GetTotalMonths(), Peso = crianca2.PesoInicial + 1 };
            var crescimento4 = new Crescimento { IdCrescimento = 4, IdCrianca = crianca2.IdCrianca, Crianca = crianca2, Altura = crianca2.AlturaInicial + 2, MesesDeIdade = crianca2.DataNascimento.GetTotalMonths(), Peso = crianca2.PesoInicial + 2 };
            var crescimento5 = new Crescimento { IdCrescimento = 5, IdCrianca = crianca3.IdCrianca, Crianca = crianca3, Altura = crianca3.AlturaInicial + 1, MesesDeIdade = crianca3.DataNascimento.GetTotalMonths(), Peso = crianca3.PesoInicial + 1 };
            var crescimento6 = new Crescimento { IdCrescimento = 6, IdCrianca = crianca3.IdCrianca, Crianca = crianca3, Altura = crianca3.AlturaInicial + 2, MesesDeIdade = crianca3.DataNascimento.GetTotalMonths(), Peso = crianca3.PesoInicial + 2 };
            var crescimento7 = new Crescimento { IdCrescimento = 7, IdCrianca = crianca4.IdCrianca, Crianca = crianca4, Altura = crianca4.AlturaInicial + 1, MesesDeIdade = crianca4.DataNascimento.GetTotalMonths(), Peso = crianca4.PesoInicial + 1 };
            var crescimento8 = new Crescimento { IdCrescimento = 8, IdCrianca = crianca4.IdCrianca, Crianca = crianca4, Altura = crianca4.AlturaInicial + 2, MesesDeIdade = crianca4.DataNascimento.GetTotalMonths(), Peso = crianca4.PesoInicial + 2 };
            var crescimento9 = new Crescimento { IdCrescimento = 9, IdCrianca = crianca5.IdCrianca, Crianca = crianca5, Altura = crianca5.AlturaInicial + 1, MesesDeIdade = crianca5.DataNascimento.GetTotalMonths(), Peso = crianca5.PesoInicial + 1 };
            var crescimento10 = new Crescimento { IdCrescimento = 10, IdCrianca = crianca5.IdCrianca, Crianca = crianca5, Altura = crianca5.AlturaInicial + 2, MesesDeIdade = crianca5.DataNascimento.GetTotalMonths(), Peso = crianca5.PesoInicial + 2 };
            var crescimento11 = new Crescimento { IdCrescimento = 11, IdCrianca = crianca6.IdCrianca, Crianca = crianca6, Altura = crianca6.AlturaInicial + 1, MesesDeIdade = crianca6.DataNascimento.GetTotalMonths(), Peso = crianca6.PesoInicial + 1 };
            var crescimento12 = new Crescimento { IdCrescimento = 12, IdCrianca = crianca6.IdCrianca, Crianca = crianca6, Altura = crianca6.AlturaInicial + 2, MesesDeIdade = crianca6.DataNascimento.GetTotalMonths(), Peso = crianca6.PesoInicial + 2 };

            /*CafeDaManha*/
            cafe_Da_Manha.Add(new Refeicao{RefeicoesItens=new List<RefeicaoItem>
                                                       {
                                                           new RefeicaoItem { Alimento = new Alimento{Nome="Pão na Chapa"}, Quantidade = 2, Medida = new Medida{Nome = "2 fatias" }},
                                                           new RefeicaoItem { Alimento = new Alimento{Nome="Suco de Laranja"}, Quantidade = 1, Medida = new Medida{Nome = "1 copo 200ml" }}
                                                       }, TiposRefeicao = TipoRefeicao.CafeDaManha});
            cafe_Da_Manha.Add(new Refeicao{RefeicoesItens=new List<RefeicaoItem>
                                                       {
                                                           new RefeicaoItem { Alimento = new Alimento{Nome="Mamão Papaia"}, Quantidade = 1, Medida = new Medida{Nome = "1 fatias pequena" }},
                                                           new RefeicaoItem { Alimento = new Alimento{Nome="Torrada Integral"}, Quantidade = 2, Medida = new Medida{Nome = "2 unidades" }}
                                                       }, TiposRefeicao = TipoRefeicao.CafeDaManha});
            
            

            /*Lanche Manhã*/
            lanche_Da_Manha.Add(new Refeicao{RefeicoesItens=new List<RefeicaoItem>
                                                       {
                                                           new RefeicaoItem { Alimento = new Alimento{Nome="Pera"}, Quantidade = 1, Medida = new Medida{Nome = "1 unidade" }},
                                                           new RefeicaoItem { Alimento = new Alimento{Nome="Suco de Uva"}, Quantidade = 1, Medida = new Medida{Nome = "1 copo 200ml" }}
                                                       }, TiposRefeicao = TipoRefeicao.LancheDaManha});
            lanche_Da_Manha.Add(new Refeicao{RefeicoesItens=new List<RefeicaoItem>
                                                       {
                                                           new RefeicaoItem { Alimento = new Alimento{Nome="Mamão Papaia"}, Quantidade = 1, Medida = new Medida{Nome = "1 fatias pequena" }}
                                                       }, TiposRefeicao = TipoRefeicao.LancheDaManha});

            /*Almoco*/
            almoco.Add(new Refeicao{RefeicoesItens=new List<RefeicaoItem>
                                                       {
                                                           new RefeicaoItem { Alimento = new Alimento{Nome="Arroz Integral"}, Quantidade = 1, Medida = new Medida{Nome = "1 concha" }},
                                                           new RefeicaoItem { Alimento = new Alimento{Nome="Feijão"}, Quantidade = 1, Medida = new Medida{Nome = "1 concha" }}
                                                       }, TiposRefeicao = TipoRefeicao.Almoco});
            almoco.Add(new Refeicao{RefeicoesItens=new List<RefeicaoItem>
                                                       {
                                                           new RefeicaoItem { Alimento = new Alimento{Nome="Filét de Frango"}, Quantidade = 200, Medida = new Medida{Nome = "200 gramas" }},
                                                           new RefeicaoItem { Alimento = new Alimento{Nome="Tomate"}, Quantidade = 4, Medida = new Medida{Nome = "4 rodelas" }},
                                                           new RefeicaoItem { Alimento = new Alimento{Nome="Alface"}, Quantidade = 2, Medida = new Medida{Nome = "2 folhas" }}
                                                       }, TiposRefeicao = TipoRefeicao.Almoco});

            /*Lanche Tarde*/
            lanche_Da_Tarde.Add(new Refeicao{RefeicoesItens=new List<RefeicaoItem>
                                                       {
                                                           new RefeicaoItem { Alimento = new Alimento{Nome="Pera"}, Quantidade = 1, Medida = new Medida{Nome = "1 unidade" }}
                                                       }, TiposRefeicao = TipoRefeicao.LancheDaTarde});
            lanche_Da_Tarde.Add(new Refeicao{RefeicoesItens=new List<RefeicaoItem>
                                                       {
                                                           new RefeicaoItem { Alimento = new Alimento{Nome="Suco de Uva"}, Quantidade = 1, Medida = new Medida{Nome = "1 copo 200ml" }},
                                                           new RefeicaoItem { Alimento = new Alimento{Nome="Mamão Papaia"}, Quantidade = 1, Medida = new Medida{Nome = "1 fatias pequena" }}
                                                       }, TiposRefeicao = TipoRefeicao.LancheDaTarde});
            
            /*Jantar*/
            jantar.Add(new Refeicao{RefeicoesItens=new List<RefeicaoItem>
                                                       {
                                                           new RefeicaoItem { Alimento = new Alimento{Nome="Arroz Branco"}, Quantidade = 1, Medida = new Medida{Nome = "1 concha" }},
                                                           new RefeicaoItem { Alimento = new Alimento{Nome="Feijão Preto"}, Quantidade = 1, Medida = new Medida{Nome = "1 concha" }}
                                                       }, TiposRefeicao = TipoRefeicao.Jantar});
            jantar.Add(new Refeicao{RefeicoesItens=new List<RefeicaoItem>
                                                       {
                                                           new RefeicaoItem { Alimento = new Alimento{Nome="Ovo Frito"}, Quantidade = 1, Medida = new Medida{Nome = "1 unidade" }}
                                                       }, TiposRefeicao = TipoRefeicao.Jantar});

            /*LANCHE_DA_NOITE*/
            lanche_Da_Noite.Add(new Refeicao{RefeicoesItens=new List<RefeicaoItem>
                                                       {
                                                           new RefeicaoItem { Alimento = new Alimento{Nome="Arroz Branco"}, Quantidade = 1, Medida = new Medida{Nome = "1 concha" }},
                                                           new RefeicaoItem { Alimento = new Alimento{Nome="Feijão Preto"}, Quantidade = 1, Medida = new Medida{Nome = "1 concha" }}
                                                       }, TiposRefeicao = TipoRefeicao.LancheDaNoite});
            lanche_Da_Noite.Add(new Refeicao{RefeicoesItens=new List<RefeicaoItem>
                                                       {
                                                           new RefeicaoItem { Alimento = new Alimento{Nome="Ovo Frito"}, Quantidade = 1, Medida = new Medida{Nome = "1 unidade" }}
                                                       }, TiposRefeicao = TipoRefeicao.LancheDaNoite});

            alimentos.Add(alimento1);
            alimentos.Add(alimento2);
            alimentos.Add(alimento3);
            alimentos.Add(alimento4);
            alimentos.Add(alimento5);

            responsaveis.Add(responsavel1);
            responsaveis.Add(responsavel2);
            responsaveis.Add(responsavel3);
            responsaveis.Add(responsavel4);
            responsaveis.Add(responsavel5);

            tokens.Add(token1);
            tokens.Add(token2);
            tokens.Add(token3);
            tokens.Add(token4);
            tokens.Add(token5);

            logins_Sociais.Add(loginSocial1);
            logins_Sociais.Add(loginSocial2);
            logins_Sociais.Add(loginSocial3);

            criancas.Add(crianca1);
            criancas.Add(crianca2);
            criancas.Add(crianca3);
            criancas.Add(crianca4);
            criancas.Add(crianca5);
            criancas.Add(crianca6);
            criancas.Add(crianca7);
            criancas.Add(crianca8);
            criancas.Add(crianca9);
            criancas.Add(crianca10);

            crescimentos.Add(crescimento1);
            crescimentos.Add(crescimento2);
            crescimentos.Add(crescimento3);
            crescimentos.Add(crescimento4);
            crescimentos.Add(crescimento5);
            crescimentos.Add(crescimento6);
            crescimentos.Add(crescimento7);
            crescimentos.Add(crescimento8);
            crescimentos.Add(crescimento9);
            crescimentos.Add(crescimento10);
            crescimentos.Add(crescimento11);
            crescimentos.Add(crescimento12);
        }
    }
}
