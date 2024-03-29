﻿using System.Text.RegularExpressions;
using BaltaIOChallenge.Core.Infrastructure;
using BaltaIOChallenge.Core.Models;

namespace BaltaIOChallenge.Core.Services;

public static class SeedService
{
    public static void Run(AppDbContext dbContext)
    {
        if (dbContext.Cities.Any()) return;
        
        foreach (var city in Cities())
        {
            dbContext.Cities.Add(city);
        }

        dbContext.SaveChangesAsync();
    }

    public static void CreateAdminUser(AppDbContext dbContext)
    {
        var admin = new User(1, "admin@admin.com", "123456");
        
        var adminAlreadyExist = dbContext.Users
            .Any(x => x.Email == admin.Email);
        
        if(adminAlreadyExist) return;

        dbContext.Users.Add(admin);
        dbContext.SaveChanges();
    }

    private static IEnumerable<City> Cities()
    {
        var id = 1;
        const string cities = @"1100015,RO,Alta Floresta Oeste
1100379,RO,Alto Alegre dos Parecis
1100403,RO,Alto Paraíso
1100346,RO,Alvorada Oeste
1100023,RO,Ariquemes
1100452,RO,Buritis
1100031,RO,Cabixi
1100601,RO,Cacaulândia
1100049,RO,Cacoal
1100700,RO,Campo Novo de Rondônia
1100809,RO,Candeias do Jamari
1100908,RO,Castanheiras
1100056,RO,Cerejeiras
1100924,RO,Chupinguaia
1100064,RO,Colorado do Oeste
1100072,RO,Corumbiara
1100080,RO,Costa Marques
1100940,RO,Cujubim
1100098,RO,Espigão Oeste
1101005,RO,Governador Jorge Teixeira
1100106,RO,Guajará-Mirim
1101104,RO,Itapuã do Oeste
1100114,RO,Jaru
1100122,RO,Ji-Paraná
1100130,RO,Machadinho Oeste
1101203,RO,Ministro Andreazza
1101302,RO,Mirante da Serra
1101401,RO,Monte Negro
1100148,RO,Nova Brasilândia Oeste
1100338,RO,Nova Mamoré
1101435,RO,Nova União
1100502,RO,Novo Horizonte do Oeste
1100155,RO,Ouro Preto do Oeste
1101450,RO,Parecis
1100189,RO,Pimenta Bueno
1101468,RO,Pimenteiras do Oeste
1100205,RO,Porto Velho
1100254,RO,Presidente Médici
1101476,RO,Primavera de Rondônia
1100262,RO,Rio Crespo
1100288,RO,Rolim de Moura
1100296,RO,Santa Luzia Oeste
1101484,RO,São Felipe Oeste
1101492,RO,São Francisco do Guaporé
1100320,RO,São Miguel do Guaporé
1101500,RO,Seringueiras
1101559,RO,Teixeirópolis
1101609,RO,Theobroma
1101708,RO,Urupá
1101757,RO,Vale do Anari
1101807,RO,Vale do Paraíso
1100304,RO,Vilhena
1200013,AC,Acrelândia
1200054,AC,Assis Brasil
1200104,AC,Brasiléia
1200138,AC,Bujari
1200179,AC,Capixaba
1200203,AC,Cruzeiro do Sul
1200252,AC,Epitaciolândia
1200302,AC,Feijó
1200328,AC,Jordão
1200336,AC,Mâncio Lima
1200344,AC,Manoel Urbano
1200351,AC,Marechal Thaumaturgo
1200385,AC,Plácido de Castro
1200807,AC,Porto Acre
1200393,AC,Porto Walter
1200401,AC,Rio Branco
1200427,AC,Rodrigues Alves
1200435,AC,Santa Rosa do Purus
1200500,AC,Sena Madureira
1200450,AC,Senador Guiomard
1200609,AC,Tarauacá
1200708,AC,Xapuri
1300029,AM,Alvarães
1300060,AM,Amaturá
1300086,AM,Anamã
1300102,AM,Anori
1300144,AM,Apuí
1300201,AM,Atalaia do Norte
1300300,AM,Autazes
1300409,AM,Barcelos
1300508,AM,Barreirinha
1300607,AM,Benjamin Constant
1300631,AM,Beruri
1300680,AM,Boa Vista do Ramos
1300706,AM,Boca do Acre
1300805,AM,Borba
1300839,AM,Caapiranga
1300904,AM,Canutama
1301001,AM,Carauari
1301100,AM,Careiro
1301159,AM,Careiro da Várzea
1301209,AM,Coari
1301308,AM,Codajás
1301407,AM,Eirunepé
1301506,AM,Envira
1301605,AM,Fonte Boa
1301654,AM,Guajará
1301704,AM,Humaitá
1301803,AM,Ipixuna
1301852,AM,Iranduba
1301902,AM,Itacoatiara
1301951,AM,Itamarati
1302009,AM,Itapiranga
1302108,AM,Japurá
1302207,AM,Juruá
1302306,AM,Jutaí
1302405,AM,Lábrea
1302504,AM,Manacapuru
1302553,AM,Manaquiri
1302603,AM,Manaus
1302702,AM,Manicoré
1302801,AM,Maraã
1302900,AM,Maués
1303007,AM,Nhamundá
1303106,AM,Nova Olinda do Norte
1303205,AM,Novo Airão
1303304,AM,Novo Aripuanã
1303403,AM,Parintins
1303502,AM,Pauini
1303536,AM,Presidente Figueiredo
1303569,AM,Rio Preto da Eva
1303601,AM,Santa Isabel do Rio Negro
1303700,AM,Santo Antônio do Içá
1303809,AM,São Gabriel da Cachoeira
1303908,AM,São Paulo de Olivença
1303957,AM,São Sebastião do Uatumã
1304005,AM,Silves
1304062,AM,Tabatinga
1304104,AM,Tapauá
1304203,AM,Tefé
1304237,AM,Tonantins
1304260,AM,Uarini
1304302,AM,Urucará
1304401,AM,Urucurituba
1400050,RO,Alto Alegre
1400027,RO,Amajari
1400100,RO,Boa Vista
1400159,RO,Bonfim
1400175,RO,Cantá
1400209,RO,Caracaraí
1400233,RO,Caroebe
1400282,RO,Iracema
1400308,RO,Mucajaí
1400407,RO,Normandia
1400456,RO,Pacaraima
1400472,RO,Rorainópolis
1400506,RO,São João da Baliza
1400605,RO,São Luiz
1400704,RO,Uiramutã
1500107,PA,Abaetetuba
1500131,PA,Abel Figueiredo
1500206,PA,Acará
1500305,PA,Afuá
1500347,PA,Água Azul do Norte
1500404,PA,Alenquer
1500503,PA,Almeirim
1500602,PA,Altamira
1500701,PA,Anajás
1500800,PA,Ananindeua
1500859,PA,Anapu
1500909,PA,Augusto Corrêa
1500958,PA,Aurora do Pará
1501006,PA,Aveiro
1501105,PA,Bagre
1501204,PA,Baião
1501253,PA,Bannach
1501303,PA,Barcarena
1501402,PA,Belém
1501451,PA,Belterra
1501501,PA,Benevides
1501576,PA,Bom Jesus do Tocantins
1501600,PA,Bonito
1501709,PA,Bragança
1501725,PA,Brasil Novo
1501758,PA,Brejo Grande do Araguaia
1501782,PA,Breu Branco
1501808,PA,Breves
1501907,PA,Bujaru
1502004,PA,Cachoeira do Arari
1501956,PA,Cachoeira do Piriá
1502103,PA,Cametá
1502152,PA,Canaã dos Carajás
1502202,PA,Capanema
1502301,PA,Capitão Poço
1502400,PA,Castanhal
1502509,PA,Chaves
1502608,PA,Colares
1502707,PA,Conceição do Araguaia
1502756,PA,Concórdia do Pará
1502764,PA,Cumaru do Norte
1502772,PA,Curionópolis
1502806,PA,Curralinho
1502855,PA,Curuá
1502905,PA,Curuçá
1502939,PA,Dom Eliseu
1502954,PA,Eldorado do Carajás
1503002,PA,Faro
1503044,PA,Floresta do Araguaia
1503077,PA,Garrafão do Norte
1503093,PA,Goianésia do Pará
1503101,PA,Gurupá
1503200,PA,Igarapé-Açu
1503309,PA,Igarapé-Miri
1503408,PA,Inhangapi
1503457,PA,Ipixuna do Pará
1503507,PA,Irituia
1503606,PA,Itaituba
1503705,PA,Itupiranga
1503754,PA,Jacareacanga
1503804,PA,Jacundá
1503903,PA,Juruti
1504000,PA,Limoeiro do Ajuru
1504059,PA,Mãe do Rio
1504109,PA,Magalhães Barata
1504208,PA,Marabá
1504307,PA,Maracanã
1504406,PA,Marapanim
1504422,PA,Marituba
1504455,PA,Medicilândia
1504505,PA,Melgaço
1504604,PA,Mocajuba
1504703,PA,Moju
1504752,PA,Mojuí dos Campos
1504802,PA,Monte Alegre
1504901,PA,Muaná
1504950,PA,Nova Esperança do Piriá
1504976,PA,Nova Ipixuna
1505007,PA,Nova Timboteua
1505031,PA,Novo Progresso
1505064,PA,Novo Repartimento
1505106,PA,Óbidos
1505205,PA,Oeiras do Pará
1505304,PA,Oriximiná
1505403,PA,Ourém
1505437,PA,Ourilândia do Norte
1505486,PA,Pacajá
1505494,PA,Palestina do Pará
1505502,PA,Paragominas
1505536,PA,Parauapebas
1505551,PA,Pau Arco
1505601,PA,Peixe-Boi
1505635,PA,Piçarra
1505650,PA,Placas
1505700,PA,Ponta de Pedras
1505809,PA,Portel
1505908,PA,Porto de Moz
1506005,PA,Prainha
1506104,PA,Primavera
1506112,PA,Quatipuru
1506138,PA,Redenção
1506161,PA,Rio Maria
1506187,PA,Rondon do Pará
1506195,PA,Rurópolis
1506203,PA,Salinópolis
1506302,PA,Salvaterra
1506351,PA,Santa Bárbara do Pará
1506401,PA,Santa Cruz do Arari
1506500,PA,Santa Izabel do Pará
1506559,PA,Santa Luzia do Pará
1506583,PA,Santa Maria das Barreiras
1506609,PA,Santa Maria do Pará
1506708,PA,Santana do Araguaia
1506807,PA,Santarém
1506906,PA,Santarém Novo
1507003,PA,Santo Antônio do Tauá
1507102,PA,São Caetano de Odivelas
1507151,PA,São Domingos do Araguaia
1507201,PA,São Domingos do Capim
1507300,PA,São Félix do Xingu
1507409,PA,São Francisco do Pará
1507458,PA,São Geraldo do Araguaia
1507466,PA,São João da Ponta
1507474,PA,São João de Pirabas
1507508,PA,São João do Araguaia
1507607,PA,São Miguel do Guamá
1507706,PA,São Sebastião da Boa Vista
1507755,PA,Sapucaia
1507805,PA,Senador José Porfírio
1507904,PA,Soure
1507953,PA,Tailândia
1507961,PA,Terra Alta
1507979,PA,Terra Santa
1508001,PA,Tomé-Açu
1508035,PA,Tracuateua
1508050,PA,Trairão
1508084,PA,Tucumã
1508100,PA,Tucuruí
1508126,PA,Ulianópolis
1508159,PA,Uruará
1508209,PA,Vigia
1508308,PA,Viseu
1508357,PA,Vitória do Xingu
1508407,PA,Xinguara
1600105,AP,Amapá
1600204,AP,Calçoene
1600212,AP,Cutias
1600238,AP,Ferreira Gomes
1600253,AP,Itaubal
1600279,AP,Laranjal do Jari
1600303,AP,Macapá
1600402,AP,Mazagão
1600501,AP,Oiapoque
1600154,AP,Pedra Branca do Amapari
1600535,AP,Porto Grande
1600550,AP,Pracuúba
1600600,AP,Santana
1600055,AP,Serra do Navio
1600709,AP,Tartarugalzinho
1600808,AP,Vitória do Jari
1700251,TO,Abreulândia
1700301,TO,Aguiarnópolis
1700350,TO,Aliança do Tocantins
1700400,TO,Almas
1700707,TO,Alvorada
1701002,TO,Ananás
1701051,TO,Angico
1701101,TO,Aparecida do Rio Negro
1701309,TO,Aragominas
1701903,TO,Araguacema
1702000,TO,Araguaçu
1702109,TO,Araguaína
1702158,TO,Araguanã
1702208,TO,Araguatins
1702307,TO,Arapoema
1702406,TO,Arraias
1702554,TO,Augustinópolis
1702703,TO,Aurora do Tocantins
1702901,TO,Axixá do Tocantins
1703008,TO,Babaçulândia
1703057,TO,Bandeirantes do Tocantins
1703073,TO,Barra do Ouro
1703107,TO,Barrolândia
1703206,TO,Bernardo Sayão
1703305,TO,Bom Jesus do Tocantins
1703602,TO,Brasilândia do Tocantins
1703701,TO,Brejinho de Nazaré
1703800,TO,Buriti do Tocantins
1703826,TO,Cachoeirinha
1703842,TO,Campos Lindos
1703867,TO,Cariri do Tocantins
1703883,TO,Carmolândia
1703891,TO,Carrasco Bonito
1703909,TO,Caseara
1704105,TO,Centenário
1705102,TO,Chapada da Natividade
1704600,TO,Chapada de Areia
1705508,TO,Colinas do Tocantins
1716703,TO,Colméia
1705557,TO,Combinado
1705607,TO,Conceição do Tocantins
1706001,TO,Couto Magalhães
1706100,TO,Cristalândia
1706258,TO,Crixás do Tocantins
1706506,TO,Darcinópolis
1707009,TO,Dianópolis
1707108,TO,Divinópolis do Tocantins
1707207,TO,Dois Irmãos do Tocantins
1707306,TO,Dueré
1707405,TO,Esperantina
1707553,TO,Fátima
1707652,TO,Figueirópolis
1707702,TO,Filadélfia
1708205,TO,Formoso do Araguaia
1708254,TO,Fortaleza do Tabocão
1708304,TO,Goianorte
1709005,TO,Goiatins
1709302,TO,Guaraí
1709500,TO,Gurupi
1709807,TO,Ipueiras
1710508,TO,Itacajá
1710706,TO,Itaguatins
1710904,TO,Itapiratins
1711100,TO,Itaporã do Tocantins
1711506,TO,Jaú do Tocantins
1711803,TO,Juarina
1711902,TO,Lagoa da Confusão
1711951,TO,Lagoa do Tocantins
1712009,TO,Lajeado
1712157,TO,Lavandeira
1712405,TO,Lizarda
1712454,TO,Luzinópolis
1712504,TO,Marianópolis do Tocantins
1712702,TO,Mateiros
1712801,TO,Maurilândia do Tocantins
1713205,TO,Miracema do Tocantins
1713304,TO,Miranorte
1713601,TO,Monte do Carmo
1713700,TO,Monte Santo do Tocantins
1713957,TO,Muricilândia
1714203,TO,Natividade
1714302,TO,Nazaré
1714880,TO,Nova Olinda
1715002,TO,Nova Rosalândia
1715101,TO,Novo Acordo
1715150,TO,Novo Alegre
1715259,TO,Novo Jardim
1715507,TO,Oliveira de Fátima
1721000,TO,Palmas
1715705,TO,Palmeirante
1713809,TO,Palmeiras do Tocantins
1715754,TO,Palmeirópolis
1716109,TO,Paraíso do Tocantins
1716208,TO,Paranã
1716307,TO,Pau Arco
1716505,TO,Pedro Afonso
1716604,TO,Peixe
1716653,TO,Pequizeiro
1717008,TO,Pindorama do Tocantins
1717206,TO,Piraquê
1717503,TO,Pium
1717800,TO,Ponte Alta do Bom Jesus
1717909,TO,Ponte Alta do Tocantins
1718006,TO,Porto Alegre do Tocantins
1718204,TO,Porto Nacional
1718303,TO,Praia Norte
1718402,TO,Presidente Kennedy
1718451,TO,Pugmil
1718501,TO,Recursolândia
1718550,TO,Riachinho
1718659,TO,Rio da Conceição
1718709,TO,Rio dos Bois
1718758,TO,Rio Sono
1718808,TO,Sampaio
1718840,TO,Sandolândia
1718865,TO,Santa Fé do Araguaia
1718881,TO,Santa Maria do Tocantins
1718899,TO,Santa Rita do Tocantins
1718907,TO,Santa Rosa do Tocantins
1719004,TO,Santa Tereza do Tocantins
1720002,TO,Santa Terezinha do Tocantins
1720101,TO,São Bento do Tocantins
1720150,TO,São Félix do Tocantins
1720200,TO,São Miguel do Tocantins
1720259,TO,São Salvador do Tocantins
1720309,TO,São Sebastião do Tocantins
1720499,TO,São Valério
1720655,TO,Silvanópolis
1720804,TO,Sítio Novo do Tocantins
1720853,TO,Sucupira
1720903,TO,Taguatinga
1720937,TO,Taipas do Tocantins
1720978,TO,Talismã
1721109,TO,Tocantínia
1721208,TO,Tocantinópolis
1721257,TO,Tupirama
1721307,TO,Tupiratins
1722081,TO,Wanderlândia
1722107,TO,Xambioá
2100055,MA,Açailândia
2100105,MA,Afonso Cunha
2100154,MA,Água Doce do Maranhão
2100204,MA,Alcântara
2100303,MA,Aldeias Altas
2100402,MA,Altamira do Maranhão
2100436,MA,Alto Alegre do Maranhão
2100477,MA,Alto Alegre do Pindaré
2100501,MA,Alto Parnaíba
2100550,MA,Amapá do Maranhão
2100600,MA,Amarante do Maranhão
2100709,MA,Anajatuba
2100808,MA,Anapurus
2100832,MA,Apicum-Açu
2100873,MA,Araguanã
2100907,MA,Araioses
2100956,MA,Arame
2101004,MA,Arari
2101103,MA,Axixá
2101202,MA,Bacabal
2101251,MA,Bacabeira
2101301,MA,Bacuri
2101350,MA,Bacurituba
2101400,MA,Balsas
2101509,MA,Barão de Grajaú
2101608,MA,Barra do Corda
2101707,MA,Barreirinhas
2101772,MA,Bela Vista do Maranhão
2101731,MA,Belágua
2101806,MA,Benedito Leite
2101905,MA,Bequimão
2101939,MA,Bernardo do Mearim
2101970,MA,Boa Vista do Gurupi
2102002,MA,Bom Jardim
2102036,MA,Bom Jesus das Selvas
2102077,MA,Bom Lugar
2102101,MA,Brejo
2102150,MA,Brejo de Areia
2102200,MA,Buriti
2102309,MA,Buriti Bravo
2102325,MA,Buriticupu
2102358,MA,Buritirana
2102374,MA,Cachoeira Grande
2102408,MA,Cajapió
2102507,MA,Cajari
2102556,MA,Campestre do Maranhão
2102606,MA,Cândido Mendes
2102705,MA,Cantanhede
2102754,MA,Capinzal do Norte
2102804,MA,Carolina
2102903,MA,Carutapera
2103000,MA,Caxias
2103109,MA,Cedral
2103125,MA,Central do Maranhão
2103158,MA,Centro do Guilherme
2103174,MA,Centro Novo do Maranhão
2103208,MA,Chapadinha
2103257,MA,Cidelândia
2103307,MA,Codó
2103406,MA,Coelho Neto
2103505,MA,Colinas
2103554,MA,Conceição do Lago-Açu
2103604,MA,Coroatá
2103703,MA,Cururupu
2103752,MA,Davinópolis
2103802,MA,Dom Pedro
2103901,MA,Duque Bacelar
2104008,MA,Esperantinópolis
2104057,MA,Estreito
2104073,MA,Feira Nova do Maranhão
2104081,MA,Fernando Falcão
2104099,MA,Formosa da Serra Negra
2104107,MA,Fortaleza dos Nogueiras
2104206,MA,Fortuna
2104305,MA,Godofredo Viana
2104404,MA,Gonçalves Dias
2104503,MA,Governador Archer
2104552,MA,Governador Edison Lobão
2104602,MA,Governador Eugênio Barros
2104628,MA,Governador Luiz Rocha
2104651,MA,Governador Newton Bello
2104677,MA,Governador Nunes Freire
2104701,MA,Graça Aranha
2104800,MA,Grajaú
2104909,MA,Guimarães
2105005,MA,Humberto de Campos
2105104,MA,Icatu
2105153,MA,Igarapé do Meio
2105203,MA,Igarapé Grande
2105302,MA,Imperatriz
2105351,MA,Itaipava do Grajaú
2105401,MA,Itapecuru Mirim
2105427,MA,Itinga do Maranhão
2105450,MA,Jatobá
2105476,MA,Jenipapo dos Vieiras
2105500,MA,João Lisboa
2105609,MA,Joselândia
2105658,MA,Junco do Maranhão
2105708,MA,Lago da Pedra
2105807,MA,Lago do Junco
2105948,MA,Lago dos Rodrigues
2105906,MA,Lago Verde
2105922,MA,Lagoa do Mato
2105963,MA,Lagoa Grande do Maranhão
2105989,MA,Lajeado Novo
2106003,MA,Lima Campos
2106102,MA,Loreto
2106201,MA,Luís Domingues
2106300,MA,Magalhães de Almeida
2106326,MA,Maracaçumé
2106359,MA,Marajá do Sena
2106375,MA,Maranhãozinho
2106409,MA,Mata Roma
2106508,MA,Matinha
2106607,MA,Matões
2106631,MA,Matões do Norte
2106672,MA,Milagres do Maranhão
2106706,MA,Mirador
2106755,MA,Miranda do Norte
2106805,MA,Mirinzal
2106904,MA,Monção
2107001,MA,Montes Altos
2107100,MA,Morros
2107209,MA,Nina Rodrigues
2107258,MA,Nova Colinas
2107308,MA,Nova Iorque
2107357,MA,Nova Olinda do Maranhão
2107407,MA,Olho Água das Cunhãs
2107456,MA,Olinda Nova do Maranhão
2107506,MA,Paço do Lumiar
2107605,MA,Palmeirândia
2107704,MA,Paraibano
2107803,MA,Parnarama
2107902,MA,Passagem Franca
2108009,MA,Pastos Bons
2108058,MA,Paulino Neves
2108108,MA,Paulo Ramos
2108207,MA,Pedreiras
2108256,MA,Pedro do Rosário
2108306,MA,Penalva
2108405,MA,Peri Mirim
2108454,MA,Peritoró
2108504,MA,Pindaré-Mirim
2108603,MA,Pinheiro
2108702,MA,Pio XII
2108801,MA,Pirapemas
2108900,MA,Poção de Pedras
2109007,MA,Porto Franco
2109056,MA,Porto Rico do Maranhão
2109106,MA,Presidente Dutra
2109205,MA,Presidente Juscelino
2109239,MA,Presidente Médici
2109270,MA,Presidente Sarney
2109304,MA,Presidente Vargas
2109403,MA,Primeira Cruz
2109452,MA,Raposa
2109502,MA,Riachão
2109551,MA,Ribamar Fiquene
2109601,MA,Rosário
2109700,MA,Sambaíba
2109759,MA,Santa Filomena do Maranhão
2109809,MA,Santa Helena
2109908,MA,Santa Inês
2110005,MA,Santa Luzia
2110039,MA,Santa Luzia do Paruá
2110104,MA,Santa Quitéria do Maranhão
2110203,MA,Santa Rita
2110237,MA,Santana do Maranhão
2110278,MA,Santo Amaro do Maranhão
2110302,MA,Santo Antônio dos Lopes
2110401,MA,São Benedito do Rio Preto
2110500,MA,São Bento
2110609,MA,São Bernardo
2110658,MA,São Domingos do Azeitão
2110708,MA,São Domingos do Maranhão
2110807,MA,São Félix de Balsas
2110856,MA,São Francisco do Brejão
2110906,MA,São Francisco do Maranhão
2111003,MA,São João Batista
2111029,MA,São João do Carú
2111052,MA,São João do Paraíso
2111078,MA,São João do Soter
2111102,MA,São João dos Patos
2111201,MA,São José de Ribamar
2111250,MA,São José dos Basílios
2111300,MA,São Luís
2111409,MA,São Luís Gonzaga do Maranhão
2111508,MA,São Mateus do Maranhão
2111532,MA,São Pedro da Água Branca
2111573,MA,São Pedro dos Crentes
2111607,MA,São Raimundo das Mangabeiras
2111631,MA,São Raimundo do Doca Bezerra
2111672,MA,São Roberto
2111706,MA,São Vicente Ferrer
2111722,MA,Satubinha
2111748,MA,Senador Alexandre Costa
2111763,MA,Senador La Rocque
2111789,MA,Serrano do Maranhão
2111805,MA,Sítio Novo
2111904,MA,Sucupira do Norte
2111953,MA,Sucupira do Riachão
2112001,MA,Tasso Fragoso
2112100,MA,Timbiras
2112209,MA,Timon
2112233,MA,Trizidela do Vale
2112274,MA,Tufilândia
2112308,MA,Tuntum
2112407,MA,Turiaçu
2112456,MA,Turilândia
2112506,MA,Tutóia
2112605,MA,Urbano Santos
2112704,MA,Vargem Grande
2112803,MA,Viana
2112852,MA,Vila Nova dos Martírios
2112902,MA,Vitória do Mearim
2113009,MA,Vitorino Freire
2114007,MA,Zé Doca
2200053,PI,Acauã
2200103,PI,Agricolândia
2200202,PI,Água Branca
2200251,PI,Alagoinha do Piauí
2200277,PI,Alegrete do Piauí
2200301,PI,Alto Longá
2200400,PI,Altos
2200459,PI,Alvorada do Gurguéia
2200509,PI,Amarante
2200608,PI,Angical do Piauí
2200707,PI,Anísio de Abreu
2200806,PI,Antônio Almeida
2200905,PI,Aroazes
2200954,PI,Aroeiras do Itaim
2201002,PI,Arraial
2201051,PI,Assunção do Piauí
2201101,PI,Avelino Lopes
2201150,PI,Baixa Grande do Ribeiro
2201176,PI,Barra Alcântara
2201200,PI,Barras
2201309,PI,Barreiras do Piauí
2201408,PI,Barro Duro
2201507,PI,Batalha
2201556,PI,Bela Vista do Piauí
2201572,PI,Belém do Piauí
2201606,PI,Beneditinos
2201705,PI,Bertolínia
2201739,PI,Betânia do Piauí
2201770,PI,Boa Hora
2201804,PI,Bocaina
2201903,PI,Bom Jesus
2201919,PI,Bom Princípio do Piauí
2201929,PI,Bonfim do Piauí
2201945,PI,Boqueirão do Piauí
2201960,PI,Brasileira
2201988,PI,Brejo do Piauí
2202000,PI,Buriti dos Lopes
2202026,PI,Buriti dos Montes
2202059,PI,Cabeceiras do Piauí
2202075,PI,Cajazeiras do Piauí
2202083,PI,Cajueiro da Praia
2202091,PI,Caldeirão Grande do Piauí
2202109,PI,Campinas do Piauí
2202117,PI,Campo Alegre do Fidalgo
2202133,PI,Campo Grande do Piauí
2202174,PI,Campo Largo do Piauí
2202208,PI,Campo Maior
2202251,PI,Canavieira
2202307,PI,Canto do Buriti
2202406,PI,Capitão de Campos
2202455,PI,Capitão Gervásio Oliveira
2202505,PI,Caracol
2202539,PI,Caraúbas do Piauí
2202554,PI,Caridade do Piauí
2202604,PI,Castelo do Piauí
2202653,PI,Caxingó
2202703,PI,Cocal
2202711,PI,Cocal de Telha
2202729,PI,Cocal dos Alves
2202737,PI,Coivaras
2202752,PI,Colônia do Gurguéia
2202778,PI,Colônia do Piauí
2202802,PI,Conceição do Canindé
2202851,PI,Coronel José Dias
2202901,PI,Corrente
2203008,PI,Cristalândia do Piauí
2203107,PI,Cristino Castro
2203206,PI,Curimatá
2203230,PI,Currais
2203271,PI,Curral Novo do Piauí
2203255,PI,Curralinhos
2203305,PI,Demerval Lobão
2203354,PI,Dirceu Arcoverde
2203404,PI,Dom Expedito Lopes
2203453,PI,Dom Inocêncio
2203420,PI,Domingos Mourão
2203503,PI,Elesbão Veloso
2203602,PI,Eliseu Martins
2203701,PI,Esperantina
2203750,PI,Fartura do Piauí
2203800,PI,Flores do Piauí
2203859,PI,Floresta do Piauí
2203909,PI,Floriano
2204006,PI,Francinópolis
2204105,PI,Francisco Ayres
2204154,PI,Francisco Macedo
2204204,PI,Francisco Santos
2204303,PI,Fronteiras
2204352,PI,Geminiano
2204402,PI,Gilbués
2204501,PI,Guadalupe
2204550,PI,Guaribas
2204600,PI,Hugo Napoleão
2204659,PI,Ilha Grande
2204709,PI,Inhuma
2204808,PI,Ipiranga do Piauí
2204907,PI,Isaías Coelho
2205003,PI,Itainópolis
2205102,PI,Itaueira
2205151,PI,Jacobina do Piauí
2205201,PI,Jaicós
2205250,PI,Jardim do Mulato
2205276,PI,Jatobá do Piauí
2205300,PI,Jerumenha
2205359,PI,João Costa
2205409,PI,Joaquim Pires
2205458,PI,Joca Marques
2205508,PI,José de Freitas
2205516,PI,Juazeiro do Piauí
2205524,PI,Júlio Borges
2205532,PI,Jurema
2205557,PI,Lagoa Alegre
2205573,PI,Lagoa de São Francisco
2205565,PI,Lagoa do Barro do Piauí
2205581,PI,Lagoa do Piauí
2205599,PI,Lagoa do Sítio
2205540,PI,Lagoinha do Piauí
2205607,PI,Landri Sales
2205706,PI,Luís Correia
2205805,PI,Luzilândia
2205854,PI,Madeiro
2205904,PI,Manoel Emídio
2205953,PI,Marcolândia
2206001,PI,Marcos Parente
2206050,PI,Massapê do Piauí
2206100,PI,Matias Olímpio
2206209,PI,Miguel Alves
2206308,PI,Miguel Leão
2206357,PI,Milton Brandão
2206407,PI,Monsenhor Gil
2206506,PI,Monsenhor Hipólito
2206605,PI,Monte Alegre do Piauí
2206654,PI,Morro Cabeça no Tempo
2206670,PI,Morro do Chapéu do Piauí
2206696,PI,Murici dos Portelas
2206704,PI,Nazaré do Piauí
2206720,PI,Nazária
2206753,PI,Nossa Senhora de Nazaré
2206803,PI,Nossa Senhora dos Remédios
2207959,PI,Nova Santa Rita
2206902,PI,Novo Oriente do Piauí
2206951,PI,Novo Santo Antônio
2207009,PI,Oeiras
2207108,PI,Olho Água do Piauí
2207207,PI,Padre Marcos
2207306,PI,Paes Landim
2207355,PI,Pajeú do Piauí
2207405,PI,Palmeira do Piauí
2207504,PI,Palmeirais
2207553,PI,Paquetá
2207603,PI,Parnaguá
2207702,PI,Parnaíba
2207751,PI,Passagem Franca do Piauí
2207777,PI,Patos do Piauí
2207793,PI,Pau Arco do Piauí
2207801,PI,Paulistana
2207850,PI,Pavussu
2207900,PI,Pedro II
2207934,PI,Pedro Laurentino
2208007,PI,Picos
2208106,PI,Pimenteiras
2208205,PI,Pio IX
2208304,PI,Piracuruca
2208403,PI,Piripiri
2208502,PI,Porto
2208551,PI,Porto Alegre do Piauí
2208601,PI,Prata do Piauí
2208650,PI,Queimada Nova
2208700,PI,Redenção do Gurguéia
2208809,PI,Regeneração
2208858,PI,Riacho Frio
2208874,PI,Ribeira do Piauí
2208908,PI,Ribeiro Gonçalves
2209005,PI,Rio Grande do Piauí
2209104,PI,Santa Cruz do Piauí
2209153,PI,Santa Cruz dos Milagres
2209203,PI,Santa Filomena
2209302,PI,Santa Luz
2209377,PI,Santa Rosa do Piauí
2209351,PI,Santana do Piauí
2209401,PI,Santo Antônio de Lisboa
2209450,PI,Santo Antônio dos Milagres
2209500,PI,Santo Inácio do Piauí
2209559,PI,São Braz do Piauí
2209609,PI,São Félix do Piauí
2209658,PI,São Francisco de Assis do Piauí
2209708,PI,São Francisco do Piauí
2209757,PI,São Gonçalo do Gurguéia
2209807,PI,São Gonçalo do Piauí
2209856,PI,São João da Canabrava
2209872,PI,São João da Fronteira
2209906,PI,São João da Serra
2209955,PI,São João da Varjota
2209971,PI,São João do Arraial
2210003,PI,São João do Piauí
2210052,PI,São José do Divino
2210102,PI,São José do Peixe
2210201,PI,São José do Piauí
2210300,PI,São Julião
2210359,PI,São Lourenço do Piauí
2210375,PI,São Luis do Piauí
2210383,PI,São Miguel da Baixa Grande
2210391,PI,São Miguel do Fidalgo
2210409,PI,São Miguel do Tapuio
2210508,PI,São Pedro do Piauí
2210607,PI,São Raimundo Nonato
2210623,PI,Sebastião Barros
2210631,PI,Sebastião Leal
2210656,PI,Sigefredo Pacheco
2210706,PI,Simões
2210805,PI,Simplício Mendes
2210904,PI,Socorro do Piauí
2210938,PI,Sussuapara
2210953,PI,Tamboril do Piauí
2210979,PI,Tanque do Piauí
2211001,PI,Teresina
2211100,PI,União
2211209,PI,Uruçuí
2211308,PI,Valença do Piauí
2211357,PI,Várzea Branca
2211407,PI,Várzea Grande
2211506,PI,Vera Mendes
2211605,PI,Vila Nova do Piauí
2211704,PI,Wall Ferraz
2300101,CE,Abaiara
2300150,CE,Acarape
2300200,CE,Acaraú
2300309,CE,Acopiara
2300408,CE,Aiuaba
2300507,CE,Alcântaras
2300606,CE,Altaneira
2300705,CE,Alto Santo
2300754,CE,Amontada
2300804,CE,Antonina do Norte
2300903,CE,Apuiarés
2301000,CE,Aquiraz
2301109,CE,Aracati
2301208,CE,Aracoiaba
2301257,CE,Ararendá
2301307,CE,Araripe
2301406,CE,Aratuba
2301505,CE,Arneiroz
2301604,CE,Assaré
2301703,CE,Aurora
2301802,CE,Baixio
2301851,CE,Banabuiú
2301901,CE,Barbalha
2301950,CE,Barreira
2302008,CE,Barro
2302057,CE,Barroquinha
2302107,CE,Baturité
2302206,CE,Beberibe
2302305,CE,Bela Cruz
2302404,CE,Boa Viagem
2302503,CE,Brejo Santo
2302602,CE,Camocim
2302701,CE,Campos Sales
2302800,CE,Canindé
2302909,CE,Capistrano
2303006,CE,Caridade
2303105,CE,Cariré
2303204,CE,Caririaçu
2303303,CE,Cariús
2303402,CE,Carnaubal
2303501,CE,Cascavel
2303600,CE,Catarina
2303659,CE,Catunda
2303709,CE,Caucaia
2303808,CE,Cedro
2303907,CE,Chaval
2303931,CE,Choró
2303956,CE,Chorozinho
2304004,CE,Coreaú
2304103,CE,Crateús
2304202,CE,Crato
2304236,CE,Croatá
2304251,CE,Cruz
2304269,CE,Deputado Irapuan Pinheiro
2304277,CE,Ererê
2304285,CE,Eusébio
2304301,CE,Farias Brito
2304350,CE,Forquilha
2304400,CE,Fortaleza
2304459,CE,Fortim
2304509,CE,Frecheirinha
2304608,CE,General Sampaio
2304657,CE,Graça
2304707,CE,Granja
2304806,CE,Granjeiro
2304905,CE,Groaíras
2304954,CE,Guaiúba
2305001,CE,Guaraciaba do Norte
2305100,CE,Guaramiranga
2305209,CE,Hidrolândia
2305233,CE,Horizonte
2305266,CE,Ibaretama
2305308,CE,Ibiapina
2305332,CE,Ibicuitinga
2305357,CE,Icapuí
2305407,CE,Icó
2305506,CE,Iguatu
2305605,CE,Independência
2305654,CE,Ipaporanga
2305704,CE,Ipaumirim
2305803,CE,Ipu
2305902,CE,Ipueiras
2306009,CE,Iracema
2306108,CE,Irauçuba
2306207,CE,Itaiçaba
2306256,CE,Itaitinga
2306306,CE,Itapajé
2306405,CE,Itapipoca
2306504,CE,Itapiúna
2306553,CE,Itarema
2306603,CE,Itatira
2306702,CE,Jaguaretama
2306801,CE,Jaguaribara
2306900,CE,Jaguaribe
2307007,CE,Jaguaruana
2307106,CE,Jardim
2307205,CE,Jati
2307254,CE,Jijoca de Jericoacoara
2307304,CE,Juazeiro do Norte
2307403,CE,Jucás
2307502,CE,Lavras da Mangabeira
2307601,CE,Limoeiro do Norte
2307635,CE,Madalena
2307650,CE,Maracanaú
2307700,CE,Maranguape
2307809,CE,Marco
2307908,CE,Martinópole
2308005,CE,Massapê
2308104,CE,Mauriti
2308203,CE,Meruoca
2308302,CE,Milagres
2308351,CE,Milhã
2308377,CE,Miraíma
2308401,CE,Missão Velha
2308500,CE,Mombaça
2308609,CE,Monsenhor Tabosa
2308708,CE,Morada Nova
2308807,CE,Moraújo
2308906,CE,Morrinhos
2309003,CE,Mucambo
2309102,CE,Mulungu
2309201,CE,Nova Olinda
2309300,CE,Nova Russas
2309409,CE,Novo Oriente
2309458,CE,Ocara
2309508,CE,Orós
2309607,CE,Pacajus
2309706,CE,Pacatuba
2309805,CE,Pacoti
2309904,CE,Pacujá
2310001,CE,Palhano
2310100,CE,Palmácia
2310209,CE,Paracuru
2310258,CE,Paraipaba
2310308,CE,Parambu
2310407,CE,Paramoti
2310506,CE,Pedra Branca
2310605,CE,Penaforte
2310704,CE,Pentecoste
2310803,CE,Pereiro
2310852,CE,Pindoretama
2310902,CE,Piquet Carneiro
2310951,CE,Pires Ferreira
2311009,CE,Poranga
2311108,CE,Porteiras
2311207,CE,Potengi
2311231,CE,Potiretama
2311264,CE,Quiterianópolis
2311306,CE,Quixadá
2311355,CE,Quixelô
2311405,CE,Quixeramobim
2311504,CE,Quixeré
2311603,CE,Redenção
2311702,CE,Reriutaba
2311801,CE,Russas
2311900,CE,Saboeiro
2311959,CE,Salitre
2312205,CE,Santa Quitéria
2312007,CE,Santana do Acaraú
2312106,CE,Santana do Cariri
2312304,CE,São Benedito
2312403,CE,São Gonçalo do Amarante
2312502,CE,São João do Jaguaribe
2312601,CE,São Luís do Curu
2312700,CE,Senador Pompeu
2312809,CE,Senador Sá
2312908,CE,Sobral
2313005,CE,Solonópole
2313104,CE,Tabuleiro do Norte
2313203,CE,Tamboril
2313252,CE,Tarrafas
2313302,CE,Tauá
2313351,CE,Tejuçuoca
2313401,CE,Tianguá
2313500,CE,Trairi
2313559,CE,Tururu
2313609,CE,Ubajara
2313708,CE,Umari
2313757,CE,Umirim
2313807,CE,Uruburetama
2313906,CE,Uruoca
2313955,CE,Varjota
2314003,CE,Várzea Alegre
2314102,CE,Viçosa do Ceará
2400109,RN,Acari
2400208,RN,Açu
2400307,RN,Afonso Bezerra
2400406,RN,Água Nova
2400505,RN,Alexandria
2400604,RN,Almino Afonso
2400703,RN,Alto do Rodrigues
2400802,RN,Angicos
2400901,RN,Antônio Martins
2401008,RN,Apodi
2401107,RN,Areia Branca
2401206,RN,Arês
2401305,RN,Augusto Severo
2401404,RN,Baía Formosa
2401453,RN,Baraúna
2401503,RN,Barcelona
2401602,RN,Bento Fernandes
2401651,RN,Bodó
2401701,RN,Bom Jesus
2401800,RN,Brejinho
2401859,RN,Caiçara do Norte
2401909,RN,Caiçara do Rio do Vento
2402006,RN,Caicó
2402105,RN,Campo Redondo
2402204,RN,Canguaretama
2402303,RN,Caraúbas
2402402,RN,Carnaúba dos Dantas
2402501,RN,Carnaubais
2402600,RN,Ceará-Mirim
2402709,RN,Cerro Corá
2402808,RN,Coronel Ezequiel
2402907,RN,Coronel João Pessoa
2403004,RN,Cruzeta
2403103,RN,Currais Novos
2403202,RN,Doutor Severiano
2403301,RN,Encanto
2403400,RN,Equador
2403509,RN,Espírito Santo
2403608,RN,Extremoz
2403707,RN,Felipe Guerra
2403756,RN,Fernando Pedroza
2403806,RN,Florânia
2403905,RN,Francisco Dantas
2404002,RN,Frutuoso Gomes
2404101,RN,Galinhos
2404200,RN,Goianinha
2404309,RN,Governador Dix-Sept Rosado
2404408,RN,Grossos
2404507,RN,Guamaré
2404606,RN,Ielmo Marinho
2404705,RN,Ipanguaçu
2404804,RN,Ipueira
2404853,RN,Itajá
2404903,RN,Itaú
2405009,RN,Jaçanã
2405108,RN,Jandaíra
2405207,RN,Janduís
2405306,RN,Januário Cicco
2405405,RN,Japi
2405504,RN,Jardim de Angicos
2405603,RN,Jardim de Piranhas
2405702,RN,Jardim do Seridó
2405801,RN,João Câmara
2405900,RN,João Dias
2406007,RN,José da Penha
2406106,RN,Jucurutu
2406155,RN,Jundiá
2406205,RN,Lagoa Anta
2406304,RN,Lagoa de Pedras
2406403,RN,Lagoa de Velhos
2406502,RN,Lagoa Nova
2406601,RN,Lagoa Salgada
2406700,RN,Lajes
2406809,RN,Lajes Pintadas
2406908,RN,Lucrécia
2407005,RN,Luís Gomes
2407104,RN,Macaíba
2407203,RN,Macau
2407252,RN,Major Sales
2407302,RN,Marcelino Vieira
2407401,RN,Martins
2407500,RN,Maxaranguape
2407609,RN,Messias Targino
2407708,RN,Montanhas
2407807,RN,Monte Alegre
2407906,RN,Monte das Gameleiras
2408003,RN,Mossoró
2408102,RN,Natal
2408201,RN,Nísia Floresta
2408300,RN,Nova Cruz
2408409,RN,Olho Água do Borges
2408508,RN,Ouro Branco
2408607,RN,Paraná
2408706,RN,Paraú
2408805,RN,Parazinho
2408904,RN,Parelhas
2403251,RN,Parnamirim
2409100,RN,Passa e Fica
2409209,RN,Passagem
2409308,RN,Patu
2409407,RN,Pau dos Ferros
2409506,RN,Pedra Grande
2409605,RN,Pedra Preta
2409704,RN,Pedro Avelino
2409803,RN,Pedro Velho
2409902,RN,Pendências
2410009,RN,Pilões
2410108,RN,Poço Branco
2410207,RN,Portalegre
2410256,RN,Porto do Mangue
2410405,RN,Pureza
2410504,RN,Rafael Fernandes
2410603,RN,Rafael Godeiro
2410702,RN,Riacho da Cruz
2410801,RN,Riacho de Santana
2410900,RN,Riachuelo
2408953,RN,Rio do Fogo
2411007,RN,Rodolfo Fernandes
2411106,RN,Ruy Barbosa
2411205,RN,Santa Cruz
2409332,RN,Santa Maria
2411403,RN,Santana do Matos
2411429,RN,Santana do Seridó
2411502,RN,Santo Antônio
2411601,RN,São Bento do Norte
2411700,RN,São Bento do Trairí
2411809,RN,São Fernando
2411908,RN,São Francisco do Oeste
2412005,RN,São Gonçalo do Amarante
2412104,RN,São João do Sabugi
2412203,RN,São José de Mipibu
2412302,RN,São José do Campestre
2412401,RN,São José do Seridó
2412500,RN,São Miguel
2412559,RN,São Miguel do Gostoso
2412609,RN,São Paulo do Potengi
2412708,RN,São Pedro
2412807,RN,São Rafael
2412906,RN,São Tomé
2413003,RN,São Vicente
2413102,RN,Senador Elói de Souza
2413201,RN,Senador Georgino Avelino
2410306,RN,Serra Caiada
2413300,RN,Serra de São Bento
2413359,RN,Serra do Mel
2413409,RN,Serra Negra do Norte
2413508,RN,Serrinha
2413557,RN,Serrinha dos Pintos
2413607,RN,Severiano Melo
2413706,RN,Sítio Novo
2413805,RN,Taboleiro Grande
2413904,RN,Taipu
2414001,RN,Tangará
2414100,RN,Tenente Ananias
2414159,RN,Tenente Laurentino Cruz
2411056,RN,Tibau
2414209,RN,Tibau do Sul
2414308,RN,Timbaúba dos Batistas
2414407,RN,Touros
2414456,RN,Triunfo Potiguar
2414506,RN,Umarizal
2414605,RN,Upanema
2414704,RN,Várzea
2414753,RN,Venha-Ver
2414803,RN,Vera Cruz
2414902,RN,Viçosa
2415008,RN,Vila Flor
2500106,PB,Água Branca
2500205,PB,Aguiar
2500304,PB,Alagoa Grande
2500403,PB,Alagoa Nova
2500502,PB,Alagoinha
2500536,PB,Alcantil
2500577,PB,Algodão de Jandaíra
2500601,PB,Alhandra
2500734,PB,Amparo
2500775,PB,Aparecida
2500809,PB,Araçagi
2500908,PB,Arara
2501005,PB,Araruna
2501104,PB,Areia
2501153,PB,Areia de Baraúnas
2501203,PB,Areial
2501302,PB,Aroeiras
2501351,PB,Assunção
2501401,PB,Baía da Traição
2501500,PB,Bananeiras
2501534,PB,Baraúna
2501609,PB,Barra de Santa Rosa
2501575,PB,Barra de Santana
2501708,PB,Barra de São Miguel
2501807,PB,Bayeux
2501906,PB,Belém
2502003,PB,Belém do Brejo do Cruz
2502052,PB,Bernardino Batista
2502102,PB,Boa Ventura
2502151,PB,Boa Vista
2502201,PB,Bom Jesus
2502300,PB,Bom Sucesso
2502409,PB,Bonito de Santa Fé
2502508,PB,Boqueirão
2502706,PB,Borborema
2502805,PB,Brejo do Cruz
2502904,PB,Brejo dos Santos
2503001,PB,Caaporã
2503100,PB,Cabaceiras
2503209,PB,Cabedelo
2503308,PB,Cachoeira dos Índios
2503407,PB,Cacimba de Areia
2503506,PB,Cacimba de Dentro
2503555,PB,Cacimbas
2503605,PB,Caiçara
2503704,PB,Cajazeiras
2503753,PB,Cajazeirinhas
2503803,PB,Caldas Brandão
2503902,PB,Camalaú
2504009,PB,Campina Grande
2504033,PB,Capim
2504074,PB,Caraúbas
2504108,PB,Carrapateira
2504157,PB,Casserengue
2504207,PB,Catingueira
2504306,PB,Catolé do Rocha
2504355,PB,Caturité
2504405,PB,Conceição
2504504,PB,Condado
2504603,PB,Conde
2504702,PB,Congo
2504801,PB,Coremas
2504850,PB,Coxixola
2504900,PB,Cruz do Espírito Santo
2505006,PB,Cubati
2505105,PB,Cuité
2505238,PB,Cuité de Mamanguape
2505204,PB,Cuitegi
2505279,PB,Curral de Cima
2505303,PB,Curral Velho
2505352,PB,Damião
2505402,PB,Desterro
2505600,PB,Diamante
2505709,PB,Dona Inês
2505808,PB,Duas Estradas
2505907,PB,Emas
2506004,PB,Esperança
2506103,PB,Fagundes
2506202,PB,Frei Martinho
2506251,PB,Gado Bravo
2506301,PB,Guarabira
2506400,PB,Gurinhém
2506509,PB,Gurjão
2506608,PB,Ibiara
2502607,PB,Igaracy
2506707,PB,Imaculada
2506806,PB,Ingá
2506905,PB,Itabaiana
2507002,PB,Itaporanga
2507101,PB,Itapororoca
2507200,PB,Itatuba
2507309,PB,Jacaraú
2507408,PB,Jericó
2507507,PB,João Pessoa
2513653,PB,Joca Claudino
2507606,PB,Juarez Távora
2507705,PB,Juazeirinho
2507804,PB,Junco do Seridó
2507903,PB,Juripiranga
2508000,PB,Juru
2508109,PB,Lagoa
2508208,PB,Lagoa de Dentro
2508307,PB,Lagoa Seca
2508406,PB,Lastro
2508505,PB,Livramento
2508554,PB,Logradouro
2508604,PB,Lucena
2508703,PB,Mãe Água
2508802,PB,Malta
2508901,PB,Mamanguape
2509008,PB,Manaíra
2509057,PB,Marcação
2509107,PB,Mari
2509156,PB,Marizópolis
2509206,PB,Massaranduba
2509305,PB,Mataraca
2509339,PB,Matinhas
2509370,PB,Mato Grosso
2509396,PB,Maturéia
2509404,PB,Mogeiro
2509503,PB,Montadas
2509602,PB,Monte Horebe
2509701,PB,Monteiro
2509800,PB,Mulungu
2509909,PB,Natuba
2510006,PB,Nazarezinho
2510105,PB,Nova Floresta
2510204,PB,Nova Olinda
2510303,PB,Nova Palmeira
2510402,PB,Olho Água
2510501,PB,Olivedos
2510600,PB,Ouro Velho
2510659,PB,Parari
2510709,PB,Passagem
2510808,PB,Patos
2510907,PB,Paulista
2511004,PB,Pedra Branca
2511103,PB,Pedra Lavrada
2511202,PB,Pedras de Fogo
2512721,PB,Pedro Régis
2511301,PB,Piancó
2511400,PB,Picuí
2511509,PB,Pilar
2511608,PB,Pilões
2511707,PB,Pilõezinhos
2511806,PB,Pirpirituba
2511905,PB,Pitimbu
2512002,PB,Pocinhos
2512036,PB,Poço Dantas
2512077,PB,Poço de José de Moura
2512101,PB,Pombal
2512200,PB,Prata
2512309,PB,Princesa Isabel
2512408,PB,Puxinanã
2512507,PB,Queimadas
2512606,PB,Quixaba
2512705,PB,Remígio
2512747,PB,Riachão
2512754,PB,Riachão do Bacamarte
2512762,PB,Riachão do Poço
2512788,PB,Riacho de Santo Antônio
2512804,PB,Riacho dos Cavalos
2512903,PB,Rio Tinto
2513000,PB,Salgadinho
2513109,PB,Salgado de São Félix
2513158,PB,Santa Cecília
2513208,PB,Santa Cruz
2513307,PB,Santa Helena
2513356,PB,Santa Inês
2513406,PB,Santa Luzia
2513703,PB,Santa Rita
2513802,PB,Santa Teresinha
2513505,PB,Santana de Mangueira
2513604,PB,Santana dos Garrotes
2513851,PB,Santo André
2513927,PB,São Bentinho
2513901,PB,São Bento
2513968,PB,São Domingos
2513943,PB,São Domingos do Cariri
2513984,PB,São Francisco
2514008,PB,São João do Cariri
2500700,PB,São João do Rio do Peixe
2514107,PB,São João do Tigre
2514206,PB,São José da Lagoa Tapada
2514305,PB,São José de Caiana
2514404,PB,São José de Espinharas
2514503,PB,São José de Piranhas
2514552,PB,São José de Princesa
2514602,PB,São José do Bonfim
2514651,PB,São José do Brejo do Cruz
2514701,PB,São José do Sabugi
2514800,PB,São José dos Cordeiros
2514453,PB,São José dos Ramos
2514909,PB,São Mamede
2515005,PB,São Miguel de Taipu
2515104,PB,São Sebastião de Lagoa de Roça
2515203,PB,São Sebastião do Umbuzeiro
2515401,PB,São Vicente do Seridó
2515302,PB,Sapé
2515500,PB,Serra Branca
2515609,PB,Serra da Raiz
2515708,PB,Serra Grande
2515807,PB,Serra Redonda
2515906,PB,Serraria
2515930,PB,Sertãozinho
2515971,PB,Sobrado
2516003,PB,Solânea
2516102,PB,Soledade
2516151,PB,Sossêgo
2516201,PB,Sousa
2516300,PB,Sumé
2516409,PB,Tacima
2516508,PB,Taperoá
2516607,PB,Tavares
2516706,PB,Teixeira
2516755,PB,Tenório
2516805,PB,Triunfo
2516904,PB,Uiraúna
2517001,PB,Umbuzeiro
2517100,PB,Várzea
2517209,PB,Vieirópolis
2505501,PB,Vista Serrana
2517407,PB,Zabelê
2600054,PE,Abreu e Lima
2600104,PE,Afogados da Ingazeira
2600203,PE,Afrânio
2600302,PE,Agrestina
2600401,PE,Água Preta
2600500,PE,Águas Belas
2600609,PE,Alagoinha
2600708,PE,Aliança
2600807,PE,Altinho
2600906,PE,Amaraji
2601003,PE,Angelim
2601052,PE,Araçoiaba
2601102,PE,Araripina
2601201,PE,Arcoverde
2601300,PE,Barra de Guabiraba
2601409,PE,Barreiros
2601508,PE,Belém de Maria
2601607,PE,Belém do São Francisco
2601706,PE,Belo Jardim
2601805,PE,Betânia
2601904,PE,Bezerros
2602001,PE,Bodocó
2602100,PE,Bom Conselho
2602209,PE,Bom Jardim
2602308,PE,Bonito
2602407,PE,Brejão
2602506,PE,Brejinho
2602605,PE,Brejo da Madre de Deus
2602704,PE,Buenos Aires
2602803,PE,Buíque
2602902,PE,Cabo de Santo Agostinho
2603009,PE,Cabrobó
2603108,PE,Cachoeirinha
2603207,PE,Caetés
2603306,PE,Calçado
2603405,PE,Calumbi
2603454,PE,Camaragibe
2603504,PE,Camocim de São Félix
2603603,PE,Camutanga
2603702,PE,Canhotinho
2603801,PE,Capoeiras
2603900,PE,Carnaíba
2603926,PE,Carnaubeira da Penha
2604007,PE,Carpina
2604106,PE,Caruaru
2604155,PE,Casinhas
2604205,PE,Catende
2604304,PE,Cedro
2604403,PE,Chã de Alegria
2604502,PE,Chã Grande
2604601,PE,Condado
2604700,PE,Correntes
2604809,PE,Cortês
2604908,PE,Cumaru
2605004,PE,Cupira
2605103,PE,Custódia
2605152,PE,Dormentes
2605202,PE,Escada
2605301,PE,Exu
2605400,PE,Feira Nova
2605459,PE,Fernando de Noronha
2605509,PE,Ferreiros
2605608,PE,Flores
2605707,PE,Floresta
2605806,PE,Frei Miguelinho
2605905,PE,Gameleira
2606002,PE,Garanhuns
2606101,PE,Glória do Goitá
2606200,PE,Goiana
2606309,PE,Granito
2606408,PE,Gravatá
2606507,PE,Iati
2606606,PE,Ibimirim
2606705,PE,Ibirajuba
2606804,PE,Igarassu
2606903,PE,Iguaracy
2607604,PE,Ilha de Itamaracá
2607000,PE,Inajá
2607109,PE,Ingazeira
2607208,PE,Ipojuca
2607307,PE,Ipubi
2607406,PE,Itacuruba
2607505,PE,Itaíba
2607653,PE,Itambé
2607703,PE,Itapetim
2607752,PE,Itapissuma
2607802,PE,Itaquitinga
2607901,PE,Jaboatão dos Guararapes
2607950,PE,Jaqueira
2608008,PE,Jataúba
2608057,PE,Jatobá
2608107,PE,João Alfredo
2608206,PE,Joaquim Nabuco
2608255,PE,Jucati
2608305,PE,Jupi
2608404,PE,Jurema
2608503,PE,Lagoa de Itaenga
2608453,PE,Lagoa do Carro
2608602,PE,Lagoa do Ouro
2608701,PE,Lagoa dos Gatos
2608750,PE,Lagoa Grande
2608800,PE,Lajedo
2608909,PE,Limoeiro
2609006,PE,Macaparana
2609105,PE,Machados
2609154,PE,Manari
2609204,PE,Maraial
2609303,PE,Mirandiba
2614303,PE,Moreilândia
2609402,PE,Moreno
2609501,PE,Nazaré da Mata
2609600,PE,Olinda
2609709,PE,Orobó
2609808,PE,Orocó
2609907,PE,Ouricuri
2610004,PE,Palmares
2610103,PE,Palmeirina
2610202,PE,Panelas
2610301,PE,Paranatama
2610400,PE,Parnamirim
2610509,PE,Passira
2610608,PE,Paudalho
2610707,PE,Paulista
2610806,PE,Pedra
2610905,PE,Pesqueira
2611002,PE,Petrolândia
2611101,PE,Petrolina
2611200,PE,Poção
2611309,PE,Pombos
2611408,PE,Primavera
2611507,PE,Quipapá
2611533,PE,Quixaba
2611606,PE,Recife
2611705,PE,Riacho das Almas
2611804,PE,Ribeirão
2611903,PE,Rio Formoso
2612000,PE,Sairé
2612109,PE,Salgadinho
2612208,PE,Salgueiro
2612307,PE,Saloá
2612406,PE,Sanharó
2612455,PE,Santa Cruz
2612471,PE,Santa Cruz da Baixa Verde
2612505,PE,Santa Cruz do Capibaribe
2612554,PE,Santa Filomena
2612604,PE,Santa Maria da Boa Vista
2612703,PE,Santa Maria do Cambucá
2612802,PE,Santa Terezinha
2612901,PE,São Benedito do Sul
2613008,PE,São Bento do Una
2613107,PE,São Caitano
2613206,PE,São João
2613305,PE,São Joaquim do Monte
2613404,PE,São José da Coroa Grande
2613503,PE,São José do Belmonte
2613602,PE,São José do Egito
2613701,PE,São Lourenço da Mata
2613800,PE,São Vicente Férrer
2613909,PE,Serra Talhada
2614006,PE,Serrita
2614105,PE,Sertânia
2614204,PE,Sirinhaém
2614402,PE,Solidão
2614501,PE,Surubim
2614600,PE,Tabira
2614709,PE,Tacaimbó
2614808,PE,Tacaratu
2614857,PE,Tamandaré
2615003,PE,Taquaritinga do Norte
2615102,PE,Terezinha
2615201,PE,Terra Nova
2615300,PE,Timbaúba
2615409,PE,Toritama
2615508,PE,Tracunhaém
2615607,PE,Trindade
2615706,PE,Triunfo
2615805,PE,Tupanatinga
2615904,PE,Tuparetama
2616001,PE,Venturosa
2616100,PE,Verdejante
2616183,PE,Vertente do Lério
2616209,PE,Vertentes
2616308,PE,Vicência
2616407,PE,Vitória de Santo Antão
2616506,PE,Xexéu
2700102,AL,Água Branca
2700201,AL,Anadia
2700300,AL,Arapiraca
2700409,AL,Atalaia
2700508,AL,Barra de Santo Antônio
2700607,AL,Barra de São Miguel
2700706,AL,Batalha
2700805,AL,Belém
2700904,AL,Belo Monte
2701001,AL,Boca da Mata
2701100,AL,Branquinha
2701209,AL,Cacimbinhas
2701308,AL,Cajueiro
2701357,AL,Campestre
2701407,AL,Campo Alegre
2701506,AL,Campo Grande
2701605,AL,Canapi
2701704,AL,Capela
2701803,AL,Carneiros
2701902,AL,Chã Preta
2702009,AL,Coité do Nóia
2702108,AL,Colônia Leopoldina
2702207,AL,Coqueiro Seco
2702306,AL,Coruripe
2702355,AL,Craíbas
2702405,AL,Delmiro Gouveia
2702504,AL,Dois Riachos
2702553,AL,Estrela de Alagoas
2702603,AL,Feira Grande
2702702,AL,Feliz Deserto
2702801,AL,Flexeiras
2702900,AL,Girau do Ponciano
2703007,AL,Ibateguara
2703106,AL,Igaci
2703205,AL,Igreja Nova
2703304,AL,Inhapi
2703403,AL,Jacaré dos Homens
2703502,AL,Jacuípe
2703601,AL,Japaratinga
2703700,AL,Jaramataia
2703759,AL,Jequiá da Praia
2703809,AL,Joaquim Gomes
2703908,AL,Jundiá
2704005,AL,Junqueiro
2704104,AL,Lagoa da Canoa
2704203,AL,Limoeiro de Anadia
2704302,AL,Maceió
2704401,AL,Major Isidoro
2704906,AL,Mar Vermelho
2704500,AL,Maragogi
2704609,AL,Maravilha
2704708,AL,Marechal Deodoro
2704807,AL,Maribondo
2705002,AL,Mata Grande
2705101,AL,Matriz de Camaragibe
2705200,AL,Messias
2705309,AL,Minador do Negrão
2705408,AL,Monteirópolis
2705507,AL,Murici
2705606,AL,Novo Lino
2705705,AL,Olho Água das Flores
2705804,AL,Olho Água do Casado
2705903,AL,Olho Água Grande
2706000,AL,Olivença
2706109,AL,Ouro Branco
2706208,AL,Palestina
2706307,AL,Palmeira dos Índios
2706406,AL,Pão de Açúcar
2706422,AL,Pariconha
2706448,AL,Paripueira
2706505,AL,Passo de Camaragibe
2706604,AL,Paulo Jacinto
2706703,AL,Penedo
2706802,AL,Piaçabuçu
2706901,AL,Pilar
2707008,AL,Pindoba
2707107,AL,Piranhas
2707206,AL,Poço das Trincheiras
2707305,AL,Porto Calvo
2707404,AL,Porto de Pedras
2707503,AL,Porto Real do Colégio
2707602,AL,Quebrangulo
2707701,AL,Rio Largo
2707800,AL,Roteiro
2707909,AL,Santa Luzia do Norte
2708006,AL,Santana do Ipanema
2708105,AL,Santana do Mundaú
2708204,AL,São Brás
2708303,AL,São José da Laje
2708402,AL,São José da Tapera
2708501,AL,São Luís do Quitunde
2708600,AL,São Miguel dos Campos
2708709,AL,São Miguel dos Milagres
2708808,AL,São Sebastião
2708907,AL,Satuba
2708956,AL,Senador Rui Palmeira
2709004,AL,Tanque Arca
2709103,AL,Taquarana
2709152,AL,Teotônio Vilela
2709202,AL,Traipu
2709301,AL,União dos Palmares
2709400,AL,Viçosa
2800100,SE,Amparo de São Francisco
2800209,SE,Aquidabã
2800308,SE,Aracaju
2800407,SE,Arauá
2800506,SE,Areia Branca
2800605,SE,Barra dos Coqueiros
2800670,SE,Boquim
2800704,SE,Brejo Grande
2801009,SE,Campo do Brito
2801108,SE,Canhoba
2801207,SE,Canindé de São Francisco
2801306,SE,Capela
2801405,SE,Carira
2801504,SE,Carmópolis
2801603,SE,Cedro de São João
2801702,SE,Cristinápolis
2801900,SE,Cumbe
2802007,SE,Divina Pastora
2802106,SE,Estância
2802205,SE,Feira Nova
2802304,SE,Frei Paulo
2802403,SE,Gararu
2802502,SE,General Maynard
2802601,SE,Gracho Cardoso
2802700,SE,Ilha das Flores
2802809,SE,Indiaroba
2802908,SE,Itabaiana
2803005,SE,Itabaianinha
2803104,SE,Itabi
2803203,SE,Itaporanga Ajuda
2803302,SE,Japaratuba
2803401,SE,Japoatã
2803500,SE,Lagarto
2803609,SE,Laranjeiras
2803708,SE,Macambira
2803807,SE,Malhada dos Bois
2803906,SE,Malhador
2804003,SE,Maruim
2804102,SE,Moita Bonita
2804201,SE,Monte Alegre de Sergipe
2804300,SE,Muribeca
2804409,SE,Neópolis
2804458,SE,Nossa Senhora Aparecida
2804508,SE,Nossa Senhora da Glória
2804607,SE,Nossa Senhora das Dores
2804706,SE,Nossa Senhora de Lourdes
2804805,SE,Nossa Senhora do Socorro
2804904,SE,Pacatuba
2805000,SE,Pedra Mole
2805109,SE,Pedrinhas
2805208,SE,Pinhão
2805307,SE,Pirambu
2805406,SE,Poço Redondo
2805505,SE,Poço Verde
2805604,SE,Porto da Folha
2805703,SE,Propriá
2805802,SE,Riachão do Dantas
2805901,SE,Riachuelo
2806008,SE,Ribeirópolis
2806107,SE,Rosário do Catete
2806206,SE,Salgado
2806305,SE,Santa Luzia do Itanhy
2806503,SE,Santa Rosa de Lima
2806404,SE,Santana do São Francisco
2806602,SE,Santo Amaro das Brotas
2806701,SE,São Cristóvão
2806800,SE,São Domingos
2806909,SE,São Francisco
2807006,SE,São Miguel do Aleixo
2807105,SE,Simão Dias
2807204,SE,Siriri
2807303,SE,Telha
2807402,SE,Tobias Barreto
2807501,SE,Tomar do Geru
2807600,SE,Umbaúba
2900108,BA,Abaíra
2900207,BA,Abaré
2900306,BA,Acajutiba
2900355,BA,Adustina
2900405,BA,Água Fria
2900603,BA,Aiquara
2900702,BA,Alagoinhas
2900801,BA,Alcobaça
2900900,BA,Almadina
2901007,BA,Amargosa
2901106,BA,Amélia Rodrigues
2901155,BA,América Dourada
2901205,BA,Anagé
2901304,BA,Andaraí
2901353,BA,Andorinha
2901403,BA,Angical
2901502,BA,Anguera
2901601,BA,Antas
2901700,BA,Antônio Cardoso
2901809,BA,Antônio Gonçalves
2901908,BA,Aporá
2901957,BA,Apuarema
2902054,BA,Araçás
2902005,BA,Aracatu
2902104,BA,Araci
2902203,BA,Aramari
2902252,BA,Arataca
2902302,BA,Aratuípe
2902401,BA,Aurelino Leal
2902500,BA,Baianópolis
2902609,BA,Baixa Grande
2902658,BA,Banzaê
2902708,BA,Barra
2902807,BA,Barra da Estiva
2902906,BA,Barra do Choça
2903003,BA,Barra do Mendes
2903102,BA,Barra do Rocha
2903201,BA,Barreiras
2903235,BA,Barro Alto
2903300,BA,Barro Preto
2903276,BA,Barrocas
2903409,BA,Belmonte
2903508,BA,Belo Campo
2903607,BA,Biritinga
2903706,BA,Boa Nova
2903805,BA,Boa Vista do Tupim
2903904,BA,Bom Jesus da Lapa
2903953,BA,Bom Jesus da Serra
2904001,BA,Boninal
2904050,BA,Bonito
2904100,BA,Boquira
2904209,BA,Botuporã
2904308,BA,Brejões
2904407,BA,Brejolândia
2904506,BA,Brotas de Macaúbas
2904605,BA,Brumado
2904704,BA,Buerarema
2904753,BA,Buritirama
2904803,BA,Caatiba
2904852,BA,Cabaceiras do Paraguaçu
2904902,BA,Cachoeira
2905008,BA,Caculé
2905107,BA,Caém
2905156,BA,Caetanos
2905206,BA,Caetité
2905305,BA,Cafarnaum
2905404,BA,Cairu
2905503,BA,Caldeirão Grande
2905602,BA,Camacan
2905701,BA,Camaçari
2905800,BA,Camamu
2905909,BA,Campo Alegre de Lourdes
2906006,BA,Campo Formoso
2906105,BA,Canápolis
2906204,BA,Canarana
2906303,BA,Canavieiras
2906402,BA,Candeal
2906501,BA,Candeias
2906600,BA,Candiba
2906709,BA,Cândido Sales
2906808,BA,Cansanção
2906824,BA,Canudos
2906857,BA,Capela do Alto Alegre
2906873,BA,Capim Grosso
2906899,BA,Caraíbas
2906907,BA,Caravelas
2907004,BA,Cardeal da Silva
2907103,BA,Carinhanha
2907202,BA,Casa Nova
2907301,BA,Castro Alves
2907400,BA,Catolândia
2907509,BA,Catu
2907558,BA,Caturama
2907608,BA,Central
2907707,BA,Chorrochó
2907806,BA,Cícero Dantas
2907905,BA,Cipó
2908002,BA,Coaraci
2908101,BA,Cocos
2908200,BA,Conceição da Feira
2908309,BA,Conceição do Almeida
2908408,BA,Conceição do Coité
2908507,BA,Conceição do Jacuípe
2908606,BA,Conde
2908705,BA,Condeúba
2908804,BA,Contendas do Sincorá
2908903,BA,Coração de Maria
2909000,BA,Cordeiros
2909109,BA,Coribe
2909208,BA,Coronel João Sá
2909307,BA,Correntina
2909406,BA,Cotegipe
2909505,BA,Cravolândia
2909604,BA,Crisópolis
2909703,BA,Cristópolis
2909802,BA,Cruz das Almas
2909901,BA,Curaçá
2910008,BA,Dário Meira
2910057,BA,Dias Ávila
2910107,BA,Dom Basílio
2910206,BA,Dom Macedo Costa
2910305,BA,Elísio Medrado
2910404,BA,Encruzilhada
2910503,BA,Entre Rios
2900504,BA,Érico Cardoso
2910602,BA,Esplanada
2910701,BA,Euclides da Cunha
2910727,BA,Eunápolis
2910750,BA,Fátima
2910776,BA,Feira da Mata
2910800,BA,Feira de Santana
2910859,BA,Filadélfia
2910909,BA,Firmino Alves
2911006,BA,Floresta Azul
2911105,BA,Formosa do Rio Preto
2911204,BA,Gandu
2911253,BA,Gavião
2911303,BA,Gentio do Ouro
2911402,BA,Glória
2911501,BA,Gongogi
2911600,BA,Governador Mangabeira
2911659,BA,Guajeru
2911709,BA,Guanambi
2911808,BA,Guaratinga
2911857,BA,Heliópolis
2911907,BA,Iaçu
2912004,BA,Ibiassucê
2912103,BA,Ibicaraí
2912202,BA,Ibicoara
2912301,BA,Ibicuí
2912400,BA,Ibipeba
2912509,BA,Ibipitanga
2912608,BA,Ibiquera
2912707,BA,Ibirapitanga
2912806,BA,Ibirapuã
2912905,BA,Ibirataia
2913002,BA,Ibitiara
2913101,BA,Ibititá
2913200,BA,Ibotirama
2913309,BA,Ichu
2913408,BA,Igaporã
2913457,BA,Igrapiúna
2913507,BA,Iguaí
2913606,BA,Ilhéus
2913705,BA,Inhambupe
2913804,BA,Ipecaetá
2913903,BA,Ipiaú
2914000,BA,Ipirá
2914109,BA,Ipupiara
2914208,BA,Irajuba
2914307,BA,Iramaia
2914406,BA,Iraquara
2914505,BA,Irará
2914604,BA,Irecê
2914653,BA,Itabela
2914703,BA,Itaberaba
2914802,BA,Itabuna
2914901,BA,Itacaré
2915007,BA,Itaeté
2915106,BA,Itagi
2915205,BA,Itagibá
2915304,BA,Itagimirim
2915353,BA,Itaguaçu da Bahia
2915403,BA,Itaju do Colônia
2915502,BA,Itajuípe
2915601,BA,Itamaraju
2915700,BA,Itamari
2915809,BA,Itambé
2915908,BA,Itanagra
2916005,BA,Itanhém
2916104,BA,Itaparica
2916203,BA,Itapé
2916302,BA,Itapebi
2916401,BA,Itapetinga
2916500,BA,Itapicuru
2916609,BA,Itapitanga
2916708,BA,Itaquara
2916807,BA,Itarantim
2916856,BA,Itatim
2916906,BA,Itiruçu
2917003,BA,Itiúba
2917102,BA,Itororó
2917201,BA,Ituaçu
2917300,BA,Ituberá
2917334,BA,Iuiu
2917359,BA,Jaborandi
2917409,BA,Jacaraci
2917508,BA,Jacobina
2917607,BA,Jaguaquara
2917706,BA,Jaguarari
2917805,BA,Jaguaripe
2917904,BA,Jandaíra
2918001,BA,Jequié
2918100,BA,Jeremoabo
2918209,BA,Jiquiriçá
2918308,BA,Jitaúna
2918357,BA,João Dourado
2918407,BA,Juazeiro
2918456,BA,Jucuruçu
2918506,BA,Jussara
2918555,BA,Jussari
2918605,BA,Jussiape
2918704,BA,Lafaiete Coutinho
2918753,BA,Lagoa Real
2918803,BA,Laje
2918902,BA,Lajedão
2919009,BA,Lajedinho
2919058,BA,Lajedo do Tabocal
2919108,BA,Lamarão
2919157,BA,Lapão
2919207,BA,Lauro de Freitas
2919306,BA,Lençóis
2919405,BA,Licínio de Almeida
2919504,BA,Livramento de Nossa Senhora
2919553,BA,Luís Eduardo Magalhães
2919603,BA,Macajuba
2919702,BA,Macarani
2919801,BA,Macaúbas
2919900,BA,Macururé
2919926,BA,Madre de Deus
2919959,BA,Maetinga
2920007,BA,Maiquinique
2920106,BA,Mairi
2920205,BA,Malhada
2920304,BA,Malhada de Pedras
2920403,BA,Manoel Vitorino
2920452,BA,Mansidão
2920502,BA,Maracás
2920601,BA,Maragogipe
2920700,BA,Maraú
2920809,BA,Marcionílio Souza
2920908,BA,Mascote
2921005,BA,Mata de São João
2921054,BA,Matina
2921104,BA,Medeiros Neto
2921203,BA,Miguel Calmon
2921302,BA,Milagres
2921401,BA,Mirangaba
2921450,BA,Mirante
2921500,BA,Monte Santo
2921609,BA,Morpará
2921708,BA,Morro do Chapéu
2921807,BA,Mortugaba
2921906,BA,Mucugê
2922003,BA,Mucuri
2922052,BA,Mulungu do Morro
2922102,BA,Mundo Novo
2922201,BA,Muniz Ferreira
2922250,BA,Muquém do São Francisco
2922300,BA,Muritiba
2922409,BA,Mutuípe
2922508,BA,Nazaré
2922607,BA,Nilo Peçanha
2922656,BA,Nordestina
2922706,BA,Nova Canaã
2922730,BA,Nova Fátima
2922755,BA,Nova Ibiá
2922805,BA,Nova Itarana
2922854,BA,Nova Redenção
2922904,BA,Nova Soure
2923001,BA,Nova Viçosa
2923035,BA,Novo Horizonte
2923050,BA,Novo Triunfo
2923100,BA,Olindina
2923209,BA,Oliveira dos Brejinhos
2923308,BA,Ouriçangas
2923357,BA,Ourolândia
2923407,BA,Palmas de Monte Alto
2923506,BA,Palmeiras
2923605,BA,Paramirim
2923704,BA,Paratinga
2923803,BA,Paripiranga
2923902,BA,Pau Brasil
2924009,BA,Paulo Afonso
2924058,BA,Pé de Serra
2924108,BA,Pedrão
2924207,BA,Pedro Alexandre
2924306,BA,Piatã
2924405,BA,Pilão Arcado
2924504,BA,Pindaí
2924603,BA,Pindobaçu
2924652,BA,Pintadas
2924678,BA,Piraí do Norte
2924702,BA,Piripá
2924801,BA,Piritiba
2924900,BA,Planaltino
2925006,BA,Planalto
2925105,BA,Poções
2925204,BA,Pojuca
2925253,BA,Ponto Novo
2925303,BA,Porto Seguro
2925402,BA,Potiraguá
2925501,BA,Prado
2925600,BA,Presidente Dutra
2925709,BA,Presidente Jânio Quadros
2925758,BA,Presidente Tancredo Neves
2925808,BA,Queimadas
2925907,BA,Quijingue
2925931,BA,Quixabeira
2925956,BA,Rafael Jambeiro
2926004,BA,Remanso
2926103,BA,Retirolândia
2926202,BA,Riachão das Neves
2926301,BA,Riachão do Jacuípe
2926400,BA,Riacho de Santana
2926509,BA,Ribeira do Amparo
2926608,BA,Ribeira do Pombal
2926657,BA,Ribeirão do Largo
2926707,BA,Rio de Contas
2926806,BA,Rio do Antônio
2926905,BA,Rio do Pires
2927002,BA,Rio Real
2927101,BA,Rodelas
2927200,BA,Ruy Barbosa
2927309,BA,Salinas da Margarida
2927408,BA,Salvador
2927507,BA,Santa Bárbara
2927606,BA,Santa Brígida
2927705,BA,Santa Cruz Cabrália
2927804,BA,Santa Cruz da Vitória
2927903,BA,Santa Inês
2928059,BA,Santa Luzia
2928109,BA,Santa Maria da Vitória
2928406,BA,Santa Rita de Cássia
2928505,BA,Santa Terezinha
2928000,BA,Santaluz
2928208,BA,Santana
2928307,BA,Santanópolis
2928604,BA,Santo Amaro
2928703,BA,Santo Antônio de Jesus
2928802,BA,Santo Estêvão
2928901,BA,São Desidério
2928950,BA,São Domingos
2929107,BA,São Felipe
2929008,BA,São Félix
2929057,BA,São Félix do Coribe
2929206,BA,São Francisco do Conde
2929255,BA,São Gabriel
2929305,BA,São Gonçalo dos Campos
2929354,BA,São José da Vitória
2929370,BA,São José do Jacuípe
2929404,BA,São Miguel das Matas
2929503,BA,São Sebastião do Passé
2929602,BA,Sapeaçu
2929701,BA,Sátiro Dias
2929750,BA,Saubara
2929800,BA,Saúde
2929909,BA,Seabra
2930006,BA,Sebastião Laranjeiras
2930105,BA,Senhor do Bonfim
2930204,BA,Sento Sé
2930154,BA,Serra do Ramalho
2930303,BA,Serra Dourada
2930402,BA,Serra Preta
2930501,BA,Serrinha
2930600,BA,Serrolândia
2930709,BA,Simões Filho
2930758,BA,Sítio do Mato
2930766,BA,Sítio do Quinto
2930774,BA,Sobradinho
2930808,BA,Souto Soares
2930907,BA,Tabocas do Brejo Velho
2931004,BA,Tanhaçu
2931053,BA,Tanque Novo
2931103,BA,Tanquinho
2931202,BA,Taperoá
2931301,BA,Tapiramutá
2931350,BA,Teixeira de Freitas
2931400,BA,Teodoro Sampaio
2931509,BA,Teofilândia
2931608,BA,Teolândia
2931707,BA,Terra Nova
2931806,BA,Tremedal
2931905,BA,Tucano
2932002,BA,Uauá
2932101,BA,Ubaíra
2932200,BA,Ubaitaba
2932309,BA,Ubatã
2932408,BA,Uibaí
2932457,BA,Umburanas
2932507,BA,Una
2932606,BA,Urandi
2932705,BA,Uruçuca
2932804,BA,Utinga
2932903,BA,Valença
2933000,BA,Valente
2933059,BA,Várzea da Roça
2933109,BA,Várzea do Poço
2933158,BA,Várzea Nova
2933174,BA,Varzedo
2933208,BA,Vera Cruz
2933257,BA,Vereda
2933307,BA,Vitória da Conquista
2933406,BA,Wagner
2933455,BA,Wanderley
2933505,BA,Wenceslau Guimarães
2933604,BA,Xique-Xique
3100104,MG,Abadia dos Dourados
3100203,MG,Abaeté
3100302,MG,Abre Campo
3100401,MG,Acaiaca
3100500,MG,Açucena
3100609,MG,Água Boa
3100708,MG,Água Comprida
3100807,MG,Aguanil
3100906,MG,Águas Formosas
3101003,MG,Águas Vermelhas
3101102,MG,Aimorés
3101201,MG,Aiuruoca
3101300,MG,Alagoa
3101409,MG,Albertina
3101508,MG,Além Paraíba
3101607,MG,Alfenas
3101631,MG,Alfredo Vasconcelos
3101706,MG,Almenara
3101805,MG,Alpercata
3101904,MG,Alpinópolis
3102001,MG,Alterosa
3102050,MG,Alto Caparaó
3153509,MG,Alto Jequitibá
3102100,MG,Alto Rio Doce
3102209,MG,Alvarenga
3102308,MG,Alvinópolis
3102407,MG,Alvorada de Minas
3102506,MG,Amparo do Serra
3102605,MG,Andradas
3102803,MG,Andrelândia
3102852,MG,Angelândia
3102902,MG,Antônio Carlos
3103009,MG,Antônio Dias
3103108,MG,Antônio Prado de Minas
3103207,MG,Araçaí
3103306,MG,Aracitaba
3103405,MG,Araçuaí
3103504,MG,Araguari
3103603,MG,Arantina
3103702,MG,Araponga
3103751,MG,Araporã
3103801,MG,Arapuá
3103900,MG,Araújos
3104007,MG,Araxá
3104106,MG,Arceburgo
3104205,MG,Arcos
3104304,MG,Areado
3104403,MG,Argirita
3104452,MG,Aricanduva
3104502,MG,Arinos
3104601,MG,Astolfo Dutra
3104700,MG,Ataléia
3104809,MG,Augusto de Lima
3104908,MG,Baependi
3105004,MG,Baldim
3105103,MG,Bambuí
3105202,MG,Bandeira
3105301,MG,Bandeira do Sul
3105400,MG,Barão de Cocais
3105509,MG,Barão de Monte Alto
3105608,MG,Barbacena
3105707,MG,Barra Longa
3105905,MG,Barroso
3106002,MG,Bela Vista de Minas
3106101,MG,Belmiro Braga
3106200,MG,Belo Horizonte
3106309,MG,Belo Oriente
3106408,MG,Belo Vale
3106507,MG,Berilo
3106655,MG,Berizal
3106606,MG,Bertópolis
3106705,MG,Betim
3106804,MG,Bias Fortes
3106903,MG,Bicas
3107000,MG,Biquinhas
3107109,MG,Boa Esperança
3107208,MG,Bocaina de Minas
3107307,MG,Bocaiúva
3107406,MG,Bom Despacho
3107505,MG,Bom Jardim de Minas
3107604,MG,Bom Jesus da Penha
3107703,MG,Bom Jesus do Amparo
3107802,MG,Bom Jesus do Galho
3107901,MG,Bom Repouso
3108008,MG,Bom Sucesso
3108107,MG,Bonfim
3108206,MG,Bonfinópolis de Minas
3108255,MG,Bonito de Minas
3108305,MG,Borda da Mata
3108404,MG,Botelhos
3108503,MG,Botumirim
3108701,MG,Brás Pires
3108552,MG,Brasilândia de Minas
3108602,MG,Brasília de Minas
3108800,MG,Braúnas
3108909,MG,Brazópolis
3109006,MG,Brumadinho
3109105,MG,Bueno Brandão
3109204,MG,Buenópolis
3109253,MG,Bugre
3109303,MG,Buritis
3109402,MG,Buritizeiro
3109451,MG,Cabeceira Grande
3109501,MG,Cabo Verde
3109600,MG,Cachoeira da Prata
3109709,MG,Cachoeira de Minas
3102704,MG,Cachoeira de Pajeú
3109808,MG,Cachoeira Dourada
3109907,MG,Caetanópolis
3110004,MG,Caeté
3110103,MG,Caiana
3110202,MG,Cajuri
3110301,MG,Caldas
3110400,MG,Camacho
3110509,MG,Camanducaia
3110608,MG,Cambuí
3110707,MG,Cambuquira
3110806,MG,Campanário
3110905,MG,Campanha
3111002,MG,Campestre
3111101,MG,Campina Verde
3111150,MG,Campo Azul
3111200,MG,Campo Belo
3111309,MG,Campo do Meio
3111408,MG,Campo Florido
3111507,MG,Campos Altos
3111606,MG,Campos Gerais
3111903,MG,Cana Verde
3111705,MG,Canaã
3111804,MG,Canápolis
3112000,MG,Candeias
3112059,MG,Cantagalo
3112109,MG,Caparaó
3112208,MG,Capela Nova
3112307,MG,Capelinha
3112406,MG,Capetinga
3112505,MG,Capim Branco
3112604,MG,Capinópolis
3112653,MG,Capitão Andrade
3112703,MG,Capitão Enéas
3112802,MG,Capitólio
3112901,MG,Caputira
3113008,MG,Caraí
3113107,MG,Caranaíba
3113206,MG,Carandaí
3113305,MG,Carangola
3113404,MG,Caratinga
3113503,MG,Carbonita
3113602,MG,Careaçu
3113701,MG,Carlos Chagas
3113800,MG,Carmésia
3113909,MG,Carmo da Cachoeira
3114006,MG,Carmo da Mata
3114105,MG,Carmo de Minas
3114204,MG,Carmo do Cajuru
3114303,MG,Carmo do Paranaíba
3114402,MG,Carmo do Rio Claro
3114501,MG,Carmópolis de Minas
3114550,MG,Carneirinho
3114600,MG,Carrancas
3114709,MG,Carvalhópolis
3114808,MG,Carvalhos
3114907,MG,Casa Grande
3115003,MG,Cascalho Rico
3115102,MG,Cássia
3115300,MG,Cataguases
3115359,MG,Catas Altas
3115409,MG,Catas Altas da Noruega
3115458,MG,Catuji
3115474,MG,Catuti
3115508,MG,Caxambu
3115607,MG,Cedro do Abaeté
3115706,MG,Central de Minas
3115805,MG,Centralina
3115904,MG,Chácara
3116001,MG,Chalé
3116100,MG,Chapada do Norte
3116159,MG,Chapada Gaúcha
3116209,MG,Chiador
3116308,MG,Cipotânea
3116407,MG,Claraval
3116506,MG,Claro dos Poções
3116605,MG,Cláudio
3116704,MG,Coimbra
3116803,MG,Coluna
3116902,MG,Comendador Gomes
3117009,MG,Comercinho
3117108,MG,Conceição da Aparecida
3115201,MG,Conceição da Barra de Minas
3117306,MG,Conceição das Alagoas
3117207,MG,Conceição das Pedras
3117405,MG,Conceição de Ipanema
3117504,MG,Conceição do Mato Dentro
3117603,MG,Conceição do Pará
3117702,MG,Conceição do Rio Verde
3117801,MG,Conceição dos Ouros
3117836,MG,Cônego Marinho
3117876,MG,Confins
3117900,MG,Congonhal
3118007,MG,Congonhas
3118106,MG,Congonhas do Norte
3118205,MG,Conquista
3118304,MG,Conselheiro Lafaiete
3118403,MG,Conselheiro Pena
3118502,MG,Consolação
3118601,MG,Contagem
3118700,MG,Coqueiral
3118809,MG,Coração de Jesus
3118908,MG,Cordisburgo
3119005,MG,Cordislândia
3119104,MG,Corinto
3119203,MG,Coroaci
3119302,MG,Coromandel
3119401,MG,Coronel Fabriciano
3119500,MG,Coronel Murta
3119609,MG,Coronel Pacheco
3119708,MG,Coronel Xavier Chaves
3119807,MG,Córrego Danta
3119906,MG,Córrego do Bom Jesus
3119955,MG,Córrego Fundo
3120003,MG,Córrego Novo
3120102,MG,Couto de Magalhães de Minas
3120151,MG,Crisólita
3120201,MG,Cristais
3120300,MG,Cristália
3120409,MG,Cristiano Otoni
3120508,MG,Cristina
3120607,MG,Crucilândia
3120706,MG,Cruzeiro da Fortaleza
3120805,MG,Cruzília
3120839,MG,Cuparaque
3120870,MG,Curral de Dentro
3120904,MG,Curvelo
3121001,MG,Datas
3121100,MG,Delfim Moreira
3121209,MG,Delfinópolis
3121258,MG,Delta
3121308,MG,Descoberto
3121407,MG,Desterro de Entre Rios
3121506,MG,Desterro do Melo
3121605,MG,Diamantina
3121704,MG,Diogo de Vasconcelos
3121803,MG,Dionísio
3121902,MG,Divinésia
3122009,MG,Divino
3122108,MG,Divino das Laranjeiras
3122207,MG,Divinolândia de Minas
3122306,MG,Divinópolis
3122355,MG,Divisa Alegre
3122405,MG,Divisa Nova
3122454,MG,Divisópolis
3122470,MG,Dom Bosco
3122504,MG,Dom Cavati
3122603,MG,Dom Joaquim
3122702,MG,Dom Silvério
3122801,MG,Dom Viçoso
3122900,MG,Dona Eusébia
3123007,MG,Dores de Campos
3123106,MG,Dores de Guanhães
3123205,MG,Dores do Indaiá
3123304,MG,Dores do Turvo
3123403,MG,Doresópolis
3123502,MG,Douradoquara
3123528,MG,Durandé
3123601,MG,Elói Mendes
3123700,MG,Engenheiro Caldas
3123809,MG,Engenheiro Navarro
3123858,MG,Entre Folhas
3123908,MG,Entre Rios de Minas
3124005,MG,Ervália
3124104,MG,Esmeraldas
3124203,MG,Espera Feliz
3124302,MG,Espinosa
3124401,MG,Espírito Santo do Dourado
3124500,MG,Estiva
3124609,MG,Estrela Dalva
3124708,MG,Estrela do Indaiá
3124807,MG,Estrela do Sul
3124906,MG,Eugenópolis
3125002,MG,Ewbank da Câmara
3125101,MG,Extrema
3125200,MG,Fama
3125309,MG,Faria Lemos
3125408,MG,Felício dos Santos
3125606,MG,Felisburgo
3125705,MG,Felixlândia
3125804,MG,Fernandes Tourinho
3125903,MG,Ferros
3125952,MG,Fervedouro
3126000,MG,Florestal
3126109,MG,Formiga
3126208,MG,Formoso
3126307,MG,Fortaleza de Minas
3126406,MG,Fortuna de Minas
3126505,MG,Francisco Badaró
3126604,MG,Francisco Dumont
3126703,MG,Francisco Sá
3126752,MG,Franciscópolis
3126802,MG,Frei Gaspar
3126901,MG,Frei Inocêncio
3126950,MG,Frei Lagonegro
3127008,MG,Fronteira
3127057,MG,Fronteira dos Vales
3127073,MG,Fruta de Leite
3127107,MG,Frutal
3127206,MG,Funilândia
3127305,MG,Galiléia
3127339,MG,Gameleiras
3127354,MG,Glaucilândia
3127370,MG,Goiabeira
3127388,MG,Goianá
3127404,MG,Gonçalves
3127503,MG,Gonzaga
3127602,MG,Gouveia
3127701,MG,Governador Valadares
3127800,MG,Grão Mogol
3127909,MG,Grupiara
3128006,MG,Guanhães
3128105,MG,Guapé
3128204,MG,Guaraciaba
3128253,MG,Guaraciama
3128303,MG,Guaranésia
3128402,MG,Guarani
3128501,MG,Guarará
3128600,MG,Guarda-Mor
3128709,MG,Guaxupé
3128808,MG,Guidoval
3128907,MG,Guimarânia
3129004,MG,Guiricema
3129103,MG,Gurinhatã
3129202,MG,Heliodora
3129301,MG,Iapu
3129400,MG,Ibertioga
3129509,MG,Ibiá
3129608,MG,Ibiaí
3129657,MG,Ibiracatu
3129707,MG,Ibiraci
3129806,MG,Ibirité
3129905,MG,Ibitiúra de Minas
3130002,MG,Ibituruna
3130051,MG,Icaraí de Minas
3130101,MG,Igarapé
3130200,MG,Igaratinga
3130309,MG,Iguatama
3130408,MG,Ijaci
3130507,MG,Ilicínea
3130556,MG,Imbé de Minas
3130606,MG,Inconfidentes
3130655,MG,Indaiabira
3130705,MG,Indianópolis
3130804,MG,Ingaí
3130903,MG,Inhapim
3131000,MG,Inhaúma
3131109,MG,Inimutaba
3131158,MG,Ipaba
3131208,MG,Ipanema
3131307,MG,Ipatinga
3131406,MG,Ipiaçu
3131505,MG,Ipuiúna
3131604,MG,Iraí de Minas
3131703,MG,Itabira
3131802,MG,Itabirinha
3131901,MG,Itabirito
3132008,MG,Itacambira
3132107,MG,Itacarambi
3132206,MG,Itaguara
3132305,MG,Itaipé
3132404,MG,Itajubá
3132503,MG,Itamarandiba
3132602,MG,Itamarati de Minas
3132701,MG,Itambacuri
3132800,MG,Itambé do Mato Dentro
3132909,MG,Itamogi
3133006,MG,Itamonte
3133105,MG,Itanhandu
3133204,MG,Itanhomi
3133303,MG,Itaobim
3133402,MG,Itapagipe
3133501,MG,Itapecerica
3133600,MG,Itapeva
3133709,MG,Itatiaiuçu
3133758,MG,Itaú de Minas
3133808,MG,Itaúna
3133907,MG,Itaverava
3134004,MG,Itinga
3134103,MG,Itueta
3134202,MG,Ituiutaba
3134301,MG,Itumirim
3134400,MG,Iturama
3134509,MG,Itutinga
3134608,MG,Jaboticatubas
3134707,MG,Jacinto
3134806,MG,Jacuí
3134905,MG,Jacutinga
3135001,MG,Jaguaraçu
3135050,MG,Jaíba
3135076,MG,Jampruca
3135100,MG,Janaúba
3135209,MG,Januária
3135308,MG,Japaraíba
3135357,MG,Japonvar
3135407,MG,Jeceaba
3135456,MG,Jenipapo de Minas
3135506,MG,Jequeri
3135605,MG,Jequitaí
3135704,MG,Jequitibá
3135803,MG,Jequitinhonha
3135902,MG,Jesuânia
3136009,MG,Joaíma
3136108,MG,Joanésia
3136207,MG,João Monlevade
3136306,MG,João Pinheiro
3136405,MG,Joaquim Felício
3136504,MG,Jordânia
3136520,MG,José Gonçalves de Minas
3136553,MG,José Raydan
3136579,MG,Josenópolis
3136652,MG,Juatuba
3136702,MG,Juiz de Fora
3136801,MG,Juramento
3136900,MG,Juruaia
3136959,MG,Juvenília
3137007,MG,Ladainha
3137106,MG,Lagamar
3137205,MG,Lagoa da Prata
3137304,MG,Lagoa dos Patos
3137403,MG,Lagoa Dourada
3137502,MG,Lagoa Formosa
3137536,MG,Lagoa Grande
3137601,MG,Lagoa Santa
3137700,MG,Lajinha
3137809,MG,Lambari
3137908,MG,Lamim
3138005,MG,Laranjal
3138104,MG,Lassance
3138203,MG,Lavras
3138302,MG,Leandro Ferreira
3138351,MG,Leme do Prado
3138401,MG,Leopoldina
3138500,MG,Liberdade
3138609,MG,Lima Duarte
3138625,MG,Limeira do Oeste
3138658,MG,Lontra
3138674,MG,Luisburgo
3138682,MG,Luislândia
3138708,MG,Luminárias
3138807,MG,Luz
3138906,MG,Machacalis
3139003,MG,Machado
3139102,MG,Madre de Deus de Minas
3139201,MG,Malacacheta
3139250,MG,Mamonas
3139300,MG,Manga
3139409,MG,Manhuaçu
3139508,MG,Manhumirim
3139607,MG,Mantena
3139805,MG,Mar de Espanha
3139706,MG,Maravilhas
3139904,MG,Maria da Fé
3140001,MG,Mariana
3140100,MG,Marilac
3140159,MG,Mário Campos
3140209,MG,Maripá de Minas
3140308,MG,Marliéria
3140407,MG,Marmelópolis
3140506,MG,Martinho Campos
3140530,MG,Martins Soares
3140555,MG,Mata Verde
3140605,MG,Materlândia
3140704,MG,Mateus Leme
3171501,MG,Mathias Lobato
3140803,MG,Matias Barbosa
3140852,MG,Matias Cardoso
3140902,MG,Matipó
3141009,MG,Mato Verde
3141108,MG,Matozinhos
3141207,MG,Matutina
3141306,MG,Medeiros
3141405,MG,Medina
3141504,MG,Mendes Pimentel
3141603,MG,Mercês
3141702,MG,Mesquita
3141801,MG,Minas Novas
3141900,MG,Minduri
3142007,MG,Mirabela
3142106,MG,Miradouro
3142205,MG,Miraí
3142254,MG,Miravânia
3142304,MG,Moeda
3142403,MG,Moema
3142502,MG,Monjolos
3142601,MG,Monsenhor Paulo
3142700,MG,Montalvânia
3142809,MG,Monte Alegre de Minas
3142908,MG,Monte Azul
3143005,MG,Monte Belo
3143104,MG,Monte Carmelo
3143153,MG,Monte Formoso
3143203,MG,Monte Santo de Minas
3143401,MG,Monte Sião
3143302,MG,Montes Claros
3143450,MG,Montezuma
3143500,MG,Morada Nova de Minas
3143609,MG,Morro da Garça
3143708,MG,Morro do Pilar
3143807,MG,Munhoz
3143906,MG,Muriaé
3144003,MG,Mutum
3144102,MG,Muzambinho
3144201,MG,Nacip Raydan
3144300,MG,Nanuque
3144359,MG,Naque
3144375,MG,Natalândia
3144409,MG,Natércia
3144508,MG,Nazareno
3144607,MG,Nepomuceno
3144656,MG,Ninheira
3144672,MG,Nova Belém
3144706,MG,Nova Era
3144805,MG,Nova Lima
3144904,MG,Nova Módica
3145000,MG,Nova Ponte
3145059,MG,Nova Porteirinha
3145109,MG,Nova Resende
3145208,MG,Nova Serrana
3136603,MG,Nova União
3145307,MG,Novo Cruzeiro
3145356,MG,Novo Oriente de Minas
3145372,MG,Novorizonte
3145406,MG,Olaria
3145455,MG,Olhos-Água
3145505,MG,Olímpio Noronha
3145604,MG,Oliveira
3145703,MG,Oliveira Fortes
3145802,MG,Onça de Pitangui
3145851,MG,Oratórios
3145877,MG,Orizânia
3145901,MG,Ouro Branco
3146008,MG,Ouro Fino
3146107,MG,Ouro Preto
3146206,MG,Ouro Verde de Minas
3146255,MG,Padre Carvalho
3146305,MG,Padre Paraíso
3146552,MG,Pai Pedro
3146404,MG,Paineiras
3146503,MG,Pains
3146602,MG,Paiva
3146701,MG,Palma
3146750,MG,Palmópolis
3146909,MG,Papagaios
3147105,MG,Pará de Minas
3147006,MG,Paracatu
3147204,MG,Paraguaçu
3147303,MG,Paraisópolis
3147402,MG,Paraopeba
3147600,MG,Passa Quatro
3147709,MG,Passa Tempo
3147808,MG,Passa Vinte
3147501,MG,Passabém
3147907,MG,Passos
3147956,MG,Patis
3148004,MG,Patos de Minas
3148103,MG,Patrocínio
3148202,MG,Patrocínio do Muriaé
3148301,MG,Paula Cândido
3148400,MG,Paulistas
3148509,MG,Pavão
3148608,MG,Peçanha
3148707,MG,Pedra Azul
3148756,MG,Pedra Bonita
3148806,MG,Pedra do Anta
3148905,MG,Pedra do Indaiá
3149002,MG,Pedra Dourada
3149101,MG,Pedralva
3149150,MG,Pedras de Maria da Cruz
3149200,MG,Pedrinópolis
3149309,MG,Pedro Leopoldo
3149408,MG,Pedro Teixeira
3149507,MG,Pequeri
3149606,MG,Pequi
3149705,MG,Perdigão
3149804,MG,Perdizes
3149903,MG,Perdões
3149952,MG,Periquito
3150000,MG,Pescador
3150109,MG,Piau
3150158,MG,Piedade de Caratinga
3150208,MG,Piedade de Ponte Nova
3150307,MG,Piedade do Rio Grande
3150406,MG,Piedade dos Gerais
3150505,MG,Pimenta
3150539,MG,Pingo Água
3150570,MG,Pintópolis
3150604,MG,Piracema
3150703,MG,Pirajuba
3150802,MG,Piranga
3150901,MG,Piranguçu
3151008,MG,Piranguinho
3151107,MG,Pirapetinga
3151206,MG,Pirapora
3151305,MG,Piraúba
3151404,MG,Pitangui
3151503,MG,Piumhi
3151602,MG,Planura
3151701,MG,Poço Fundo
3151800,MG,Poços de Caldas
3151909,MG,Pocrane
3152006,MG,Pompéu
3152105,MG,Ponte Nova
3152131,MG,Ponto Chique
3152170,MG,Ponto dos Volantes
3152204,MG,Porteirinha
3152303,MG,Porto Firme
3152402,MG,Poté
3152501,MG,Pouso Alegre
3152600,MG,Pouso Alto
3152709,MG,Prados
3152808,MG,Prata
3152907,MG,Pratápolis
3153004,MG,Pratinha
3153103,MG,Presidente Bernardes
3153202,MG,Presidente Juscelino
3153301,MG,Presidente Kubitschek
3153400,MG,Presidente Olegário
3153608,MG,Prudente de Morais
3153707,MG,Quartel Geral
3153806,MG,Queluzito
3153905,MG,Raposos
3154002,MG,Raul Soares
3154101,MG,Recreio
3154150,MG,Reduto
3154200,MG,Resende Costa
3154309,MG,Resplendor
3154408,MG,Ressaquinha
3154457,MG,Riachinho
3154507,MG,Riacho dos Machados
3154606,MG,Ribeirão das Neves
3154705,MG,Ribeirão Vermelho
3154804,MG,Rio Acima
3154903,MG,Rio Casca
3155108,MG,Rio do Prado
3155009,MG,Rio Doce
3155207,MG,Rio Espera
3155306,MG,Rio Manso
3155405,MG,Rio Novo
3155504,MG,Rio Paranaíba
3155603,MG,Rio Pardo de Minas
3155702,MG,Rio Piracicaba
3155801,MG,Rio Pomba
3155900,MG,Rio Preto
3156007,MG,Rio Vermelho
3156106,MG,Ritápolis
3156205,MG,Rochedo de Minas
3156304,MG,Rodeiro
3156403,MG,Romaria
3156452,MG,Rosário da Limeira
3156502,MG,Rubelita
3156601,MG,Rubim
3156700,MG,Sabará
3156809,MG,Sabinópolis
3156908,MG,Sacramento
3157005,MG,Salinas
3157104,MG,Salto da Divisa
3157203,MG,Santa Bárbara
3157252,MG,Santa Bárbara do Leste
3157278,MG,Santa Bárbara do Monte Verde
3157302,MG,Santa Bárbara do Tugúrio
3157336,MG,Santa Cruz de Minas
3157377,MG,Santa Cruz de Salinas
3157401,MG,Santa Cruz do Escalvado
3157500,MG,Santa Efigênia de Minas
3157609,MG,Santa Fé de Minas
3157658,MG,Santa Helena de Minas
3157708,MG,Santa Juliana
3157807,MG,Santa Luzia
3157906,MG,Santa Margarida
3158003,MG,Santa Maria de Itabira
3158102,MG,Santa Maria do Salto
3158201,MG,Santa Maria do Suaçuí
3159209,MG,Santa Rita de Caldas
3159407,MG,Santa Rita de Ibitipoca
3159308,MG,Santa Rita de Jacutinga
3159357,MG,Santa Rita de Minas
3159506,MG,Santa Rita do Itueto
3159605,MG,Santa Rita do Sapucaí
3159704,MG,Santa Rosa da Serra
3159803,MG,Santa Vitória
3158300,MG,Santana da Vargem
3158409,MG,Santana de Cataguases
3158508,MG,Santana de Pirapama
3158607,MG,Santana do Deserto
3158706,MG,Santana do Garambéu
3158805,MG,Santana do Jacaré
3158904,MG,Santana do Manhuaçu
3158953,MG,Santana do Paraíso
3159001,MG,Santana do Riacho
3159100,MG,Santana dos Montes
3159902,MG,Santo Antônio do Amparo
3160009,MG,Santo Antônio do Aventureiro
3160108,MG,Santo Antônio do Grama
3160207,MG,Santo Antônio do Itambé
3160306,MG,Santo Antônio do Jacinto
3160405,MG,Santo Antônio do Monte
3160454,MG,Santo Antônio do Retiro
3160504,MG,Santo Antônio do Rio Abaixo
3160603,MG,Santo Hipólito
3160702,MG,Santos Dumont
3160801,MG,São Bento Abade
3160900,MG,São Brás do Suaçuí
3160959,MG,São Domingos das Dores
3161007,MG,São Domingos do Prata
3161056,MG,São Félix de Minas
3161106,MG,São Francisco
3161205,MG,São Francisco de Paula
3161304,MG,São Francisco de Sales
3161403,MG,São Francisco do Glória
3161502,MG,São Geraldo
3161601,MG,São Geraldo da Piedade
3161650,MG,São Geraldo do Baixio
3161700,MG,São Gonçalo do Abaeté
3161809,MG,São Gonçalo do Pará
3161908,MG,São Gonçalo do Rio Abaixo
3125507,MG,São Gonçalo do Rio Preto
3162005,MG,São Gonçalo do Sapucaí
3162104,MG,São Gotardo
3162203,MG,São João Batista do Glória
3162252,MG,São João da Lagoa
3162302,MG,São João da Mata
3162401,MG,São João da Ponte
3162450,MG,São João das Missões
3162500,MG,São João del Rei
3162559,MG,São João do Manhuaçu
3162575,MG,São João do Manteninha
3162609,MG,São João do Oriente
3162658,MG,São João do Pacuí
3162708,MG,São João do Paraíso
3162807,MG,São João Evangelista
3162906,MG,São João Nepomuceno
3162922,MG,São Joaquim de Bicas
3162948,MG,São José da Barra
3162955,MG,São José da Lapa
3163003,MG,São José da Safira
3163102,MG,São José da Varginha
3163201,MG,São José do Alegre
3163300,MG,São José do Divino
3163409,MG,São José do Goiabal
3163508,MG,São José do Jacuri
3163607,MG,São José do Mantimento
3163706,MG,São Lourenço
3163805,MG,São Miguel do Anta
3163904,MG,São Pedro da União
3164100,MG,São Pedro do Suaçuí
3164001,MG,São Pedro dos Ferros
3164209,MG,São Romão
3164308,MG,São Roque de Minas
3164407,MG,São Sebastião da Bela Vista
3164431,MG,São Sebastião da Vargem Alegre
3164472,MG,São Sebastião do Anta
3164506,MG,São Sebastião do Maranhão
3164605,MG,São Sebastião do Oeste
3164704,MG,São Sebastião do Paraíso
3164803,MG,São Sebastião do Rio Preto
3164902,MG,São Sebastião do Rio Verde
3165206,MG,São Thomé das Letras
3165008,MG,São Tiago
3165107,MG,São Tomás de Aquino
3165305,MG,São Vicente de Minas
3165404,MG,Sapucaí-Mirim
3165503,MG,Sardoá
3165537,MG,Sarzedo
3165560,MG,Sem-Peixe
3165578,MG,Senador Amaral
3165602,MG,Senador Cortes
3165701,MG,Senador Firmino
3165800,MG,Senador José Bento
3165909,MG,Senador Modestino Gonçalves
3166006,MG,Senhora de Oliveira
3166105,MG,Senhora do Porto
3166204,MG,Senhora dos Remédios
3166303,MG,Sericita
3166402,MG,Seritinga
3166501,MG,Serra Azul de Minas
3166600,MG,Serra da Saudade
3166808,MG,Serra do Salitre
3166709,MG,Serra dos Aimorés
3166907,MG,Serrania
3166956,MG,Serranópolis de Minas
3167004,MG,Serranos
3167103,MG,Serro
3167202,MG,Sete Lagoas
3165552,MG,Setubinha
3167301,MG,Silveirânia
3167400,MG,Silvianópolis
3167509,MG,Simão Pereira
3167608,MG,Simonésia
3167707,MG,Sobrália
3167806,MG,Soledade de Minas
3167905,MG,Tabuleiro
3168002,MG,Taiobeiras
3168051,MG,Taparuba
3168101,MG,Tapira
3168200,MG,Tapiraí
3168309,MG,Taquaraçu de Minas
3168408,MG,Tarumirim
3168507,MG,Teixeiras
3168606,MG,Teófilo Otoni
3168705,MG,Timóteo
3168804,MG,Tiradentes
3168903,MG,Tiros
3169000,MG,Tocantins
3169059,MG,Tocos do Moji
3169109,MG,Toledo
3169208,MG,Tombos
3169307,MG,Três Corações
3169356,MG,Três Marias
3169406,MG,Três Pontas
3169505,MG,Tumiritinga
3169604,MG,Tupaciguara
3169703,MG,Turmalina
3169802,MG,Turvolândia
3169901,MG,Ubá
3170008,MG,Ubaí
3170057,MG,Ubaporanga
3170107,MG,Uberaba
3170206,MG,Uberlândia
3170305,MG,Umburatiba
3170404,MG,Unaí
3170438,MG,União de Minas
3170479,MG,Uruana de Minas
3170503,MG,Urucânia
3170529,MG,Urucuia
3170578,MG,Vargem Alegre
3170602,MG,Vargem Bonita
3170651,MG,Vargem Grande do Rio Pardo
3170701,MG,Varginha
3170750,MG,Varjão de Minas
3170800,MG,Várzea da Palma
3170909,MG,Varzelândia
3171006,MG,Vazante
3171030,MG,Verdelândia
3171071,MG,Veredinha
3171105,MG,Veríssimo
3171154,MG,Vermelho Novo
3171204,MG,Vespasiano
3171303,MG,Viçosa
3171402,MG,Vieiras
3171600,MG,Virgem da Lapa
3171709,MG,Virgínia
3171808,MG,Virginópolis
3171907,MG,Virgolândia
3172004,MG,Visconde do Rio Branco
3172103,MG,Volta Grande
3172202,MG,Wenceslau Braz
3200102,ES,Afonso Cláudio
3200169,ES,Água Doce do Norte
3200136,ES,Águia Branca
3200201,ES,Alegre
3200300,ES,Alfredo Chaves
3200359,ES,Alto Rio Novo
3200409,ES,Anchieta
3200508,ES,Apiacá
3200607,ES,Aracruz
3200706,ES,Atílio Vivacqua
3200805,ES,Baixo Guandu
3200904,ES,Barra de São Francisco
3201001,ES,Boa Esperança
3201100,ES,Bom Jesus do Norte
3201159,ES,Brejetuba
3201209,ES,Cachoeiro de Itapemirim
3201308,ES,Cariacica
3201407,ES,Castelo
3201506,ES,Colatina
3201605,ES,Conceição da Barra
3201704,ES,Conceição do Castelo
3201803,ES,Divino de São Lourenço
3201902,ES,Domingos Martins
3202009,ES,Dores do Rio Preto
3202108,ES,Ecoporanga
3202207,ES,Fundão
3202256,ES,Governador Lindenberg
3202306,ES,Guaçuí
3202405,ES,Guarapari
3202454,ES,Ibatiba
3202504,ES,Ibiraçu
3202553,ES,Ibitirama
3202603,ES,Iconha
3202652,ES,Irupi
3202702,ES,Itaguaçu
3202801,ES,Itapemirim
3202900,ES,Itarana
3203007,ES,Iúna
3203056,ES,Jaguaré
3203106,ES,Jerônimo Monteiro
3203130,ES,João Neiva
3203163,ES,Laranja da Terra
3203205,ES,Linhares
3203304,ES,Mantenópolis
3203320,ES,Marataízes
3203346,ES,Marechal Floriano
3203353,ES,Marilândia
3203403,ES,Mimoso do Sul
3203502,ES,Montanha
3203601,ES,Mucurici
3203700,ES,Muniz Freire
3203809,ES,Muqui
3203908,ES,Nova Venécia
3204005,ES,Pancas
3204054,ES,Pedro Canário
3204104,ES,Pinheiros
3204203,ES,Piúma
3204252,ES,Ponto Belo
3204302,ES,Presidente Kennedy
3204351,ES,Rio Bananal
3204401,ES,Rio Novo do Sul
3204500,ES,Santa Leopoldina
3204559,ES,Santa Maria de Jetibá
3204609,ES,Santa Teresa
3204658,ES,São Domingos do Norte
3204708,ES,São Gabriel da Palha
3204807,ES,São José do Calçado
3204906,ES,São Mateus
3204955,ES,São Roque do Canaã
3205002,ES,Serra
3205010,ES,Sooretama
3205036,ES,Vargem Alta
3205069,ES,Venda Nova do Imigrante
3205101,ES,Viana
3205150,ES,Vila Pavão
3205176,ES,Vila Valério
3205200,ES,Vila Velha
3205309,ES,Vitória
3300100,RJ,Angra dos Reis
3300159,RJ,Aperibé
3300209,RJ,Araruama
3300225,RJ,Areal
3300233,RJ,Armação dos Búzios
3300258,RJ,Arraial do Cabo
3300308,RJ,Barra do Piraí
3300407,RJ,Barra Mansa
3300456,RJ,Belford Roxo
3300506,RJ,Bom Jardim
3300605,RJ,Bom Jesus do Itabapoana
3300704,RJ,Cabo Frio
3300803,RJ,Cachoeiras de Macacu
3300902,RJ,Cambuci
3301009,RJ,Campos dos Goytacazes
3301108,RJ,Cantagalo
3300936,RJ,Carapebus
3301157,RJ,Cardoso Moreira
3301207,RJ,Carmo
3301306,RJ,Casimiro de Abreu
3300951,RJ,Comendador Levy Gasparian
3301405,RJ,Conceição de Macabu
3301504,RJ,Cordeiro
3301603,RJ,Duas Barras
3301702,RJ,Duque de Caxias
3301801,RJ,Engenheiro Paulo de Frontin
3301850,RJ,Guapimirim
3301876,RJ,Iguaba Grande
3301900,RJ,Itaboraí
3302007,RJ,Itaguaí
3302056,RJ,Italva
3302106,RJ,Itaocara
3302205,RJ,Itaperuna
3302254,RJ,Itatiaia
3302270,RJ,Japeri
3302304,RJ,Laje do Muriaé
3302403,RJ,Macaé
3302452,RJ,Macuco
3302502,RJ,Magé
3302601,RJ,Mangaratiba
3302700,RJ,Maricá
3302809,RJ,Mendes
3302858,RJ,Mesquita
3302908,RJ,Miguel Pereira
3303005,RJ,Miracema
3303104,RJ,Natividade
3303203,RJ,Nilópolis
3303302,RJ,Niterói
3303401,RJ,Nova Friburgo
3303500,RJ,Nova Iguaçu
3303609,RJ,Paracambi
3303708,RJ,Paraíba do Sul
3303807,RJ,Paraty
3303856,RJ,Paty do Alferes
3303906,RJ,Petrópolis
3303955,RJ,Pinheiral
3304003,RJ,Piraí
3304102,RJ,Porciúncula
3304110,RJ,Porto Real
3304128,RJ,Quatis
3304144,RJ,Queimados
3304151,RJ,Quissamã
3304201,RJ,Resende
3304300,RJ,Rio Bonito
3304409,RJ,Rio Claro
3304508,RJ,Rio das Flores
3304524,RJ,Rio das Ostras
3304557,RJ,Rio de Janeiro
3304607,RJ,Santa Maria Madalena
3304706,RJ,Santo Antônio de Pádua
3304805,RJ,São Fidélis
3304755,RJ,São Francisco de Itabapoana
3304904,RJ,São Gonçalo
3305000,RJ,São João da Barra
3305109,RJ,São João de Meriti
3305133,RJ,São José de Ubá
3305158,RJ,São José do Vale do Rio Preto
3305208,RJ,São Pedro da Aldeia
3305307,RJ,São Sebastião do Alto
3305406,RJ,Sapucaia
3305505,RJ,Saquarema
3305554,RJ,Seropédica
3305604,RJ,Silva Jardim
3305703,RJ,Sumidouro
3305752,RJ,Tanguá
3305802,RJ,Teresópolis
3305901,RJ,Trajano de Moraes
3306008,RJ,Três Rios
3306107,RJ,Valença
3306156,RJ,Varre-Sai
3306206,RJ,Vassouras
3306305,RJ,Volta Redonda
3500105,SP,Adamantina
3500204,SP,Adolfo
3500303,SP,Aguaí
3500402,SP,Águas da Prata
3500501,SP,Águas de Lindóia
3500550,SP,Águas de Santa Bárbara
3500600,SP,Águas de São Pedro
3500709,SP,Agudos
3500758,SP,Alambari
3500808,SP,Alfredo Marcondes
3500907,SP,Altair
3501004,SP,Altinópolis
3501103,SP,Alto Alegre
3501152,SP,Alumínio
3501202,SP,Álvares Florence
3501301,SP,Álvares Machado
3501400,SP,Álvaro de Carvalho
3501509,SP,Alvinlândia
3501608,SP,Americana
3501707,SP,Américo Brasiliense
3501806,SP,Américo de Campos
3501905,SP,Amparo
3502002,SP,Analândia
3502101,SP,Andradina
3502200,SP,Angatuba
3502309,SP,Anhembi
3502408,SP,Anhumas
3502507,SP,Aparecida
3502606,SP,Aparecida Oeste
3502705,SP,Apiaí
3502754,SP,Araçariguama
3502804,SP,Araçatuba
3502903,SP,Araçoiaba da Serra
3503000,SP,Aramina
3503109,SP,Arandu
3503158,SP,Arapeí
3503208,SP,Araraquara
3503307,SP,Araras
3503356,SP,Arco-Íris
3503406,SP,Arealva
3503505,SP,Areias
3503604,SP,Areiópolis
3503703,SP,Ariranha
3503802,SP,Artur Nogueira
3503901,SP,Arujá
3503950,SP,Aspásia
3504008,SP,Assis
3504107,SP,Atibaia
3504206,SP,Auriflama
3504305,SP,Avaí
3504404,SP,Avanhandava
3504503,SP,Avaré
3504602,SP,Bady Bassitt
3504701,SP,Balbinos
3504800,SP,Bálsamo
3504909,SP,Bananal
3505005,SP,Barão de Antonina
3505104,SP,Barbosa
3505203,SP,Bariri
3505302,SP,Barra Bonita
3505351,SP,Barra do Chapéu
3505401,SP,Barra do Turvo
3505500,SP,Barretos
3505609,SP,Barrinha
3505708,SP,Barueri
3505807,SP,Bastos
3505906,SP,Batatais
3506003,SP,Bauru
3506102,SP,Bebedouro
3506201,SP,Bento de Abreu
3506300,SP,Bernardino de Campos
3506359,SP,Bertioga
3506409,SP,Bilac
3506508,SP,Birigui
3506607,SP,Biritiba Mirim
3506706,SP,Boa Esperança do Sul
3506805,SP,Bocaina
3506904,SP,Bofete
3507001,SP,Boituva
3507100,SP,Bom Jesus dos Perdões
3507159,SP,Bom Sucesso de Itararé
3507209,SP,Borá
3507308,SP,Boracéia
3507407,SP,Borborema
3507456,SP,Borebi
3507506,SP,Botucatu
3507605,SP,Bragança Paulista
3507704,SP,Braúna
3507753,SP,Brejo Alegre
3507803,SP,Brodowski
3507902,SP,Brotas
3508009,SP,Buri
3508108,SP,Buritama
3508207,SP,Buritizal
3508306,SP,Cabrália Paulista
3508405,SP,Cabreúva
3508504,SP,Caçapava
3508603,SP,Cachoeira Paulista
3508702,SP,Caconde
3508801,SP,Cafelândia
3508900,SP,Caiabu
3509007,SP,Caieiras
3509106,SP,Caiuá
3509205,SP,Cajamar
3509254,SP,Cajati
3509304,SP,Cajobi
3509403,SP,Cajuru
3509452,SP,Campina do Monte Alegre
3509502,SP,Campinas
3509601,SP,Campo Limpo Paulista
3509700,SP,Campos do Jordão
3509809,SP,Campos Novos Paulista
3509908,SP,Cananéia
3509957,SP,Canas
3510005,SP,Cândido Mota
3510104,SP,Cândido Rodrigues
3510153,SP,Canitar
3510203,SP,Capão Bonito
3510302,SP,Capela do Alto
3510401,SP,Capivari
3510500,SP,Caraguatatuba
3510609,SP,Carapicuíba
3510708,SP,Cardoso
3510807,SP,Casa Branca
3510906,SP,Cássia dos Coqueiros
3511003,SP,Castilho
3511102,SP,Catanduva
3511201,SP,Catiguá
3511300,SP,Cedral
3511409,SP,Cerqueira César
3511508,SP,Cerquilho
3511607,SP,Cesário Lange
3511706,SP,Charqueada
3557204,SP,Chavantes
3511904,SP,Clementina
3512001,SP,Colina
3512100,SP,Colômbia
3512209,SP,Conchal
3512308,SP,Conchas
3512407,SP,Cordeirópolis
3512506,SP,Coroados
3512605,SP,Coronel Macedo
3512704,SP,Corumbataí
3512803,SP,Cosmópolis
3512902,SP,Cosmorama
3513009,SP,Cotia
3513108,SP,Cravinhos
3513207,SP,Cristais Paulista
3513306,SP,Cruzália
3513405,SP,Cruzeiro
3513504,SP,Cubatão
3513603,SP,Cunha
3513702,SP,Descalvado
3513801,SP,Diadema
3513850,SP,Dirce Reis
3513900,SP,Divinolândia
3514007,SP,Dobrada
3514106,SP,Dois Córregos
3514205,SP,Dolcinópolis
3514304,SP,Dourado
3514403,SP,Dracena
3514502,SP,Duartina
3514601,SP,Dumont
3514700,SP,Echaporã
3514809,SP,Eldorado
3514908,SP,Elias Fausto
3514924,SP,Elisiário
3514957,SP,Embaúba
3515004,SP,Embu das Artes
3515103,SP,Embu-Guaçu
3515129,SP,Emilianópolis
3515152,SP,Engenheiro Coelho
3515186,SP,Espírito Santo do Pinhal
3515194,SP,Espírito Santo do Turvo
3557303,SP,Estiva Gerbi
3515301,SP,Estrela do Norte
3515202,SP,Estrela Oeste
3515350,SP,Euclides da Cunha Paulista
3515400,SP,Fartura
3515608,SP,Fernando Prestes
3515509,SP,Fernandópolis
3515657,SP,Fernão
3515707,SP,Ferraz de Vasconcelos
3515806,SP,Flora Rica
3515905,SP,Floreal
3516002,SP,Flórida Paulista
3516101,SP,Florínea
3516200,SP,Franca
3516309,SP,Francisco Morato
3516408,SP,Franco da Rocha
3516507,SP,Gabriel Monteiro
3516606,SP,Gália
3516705,SP,Garça
3516804,SP,Gastão Vidigal
3516853,SP,Gavião Peixoto
3516903,SP,General Salgado
3517000,SP,Getulina
3517109,SP,Glicério
3517208,SP,Guaiçara
3517307,SP,Guaimbê
3517406,SP,Guaíra
3517505,SP,Guapiaçu
3517604,SP,Guapiara
3517703,SP,Guará
3517802,SP,Guaraçaí
3517901,SP,Guaraci
3518008,SP,Guarani Oeste
3518107,SP,Guarantã
3518206,SP,Guararapes
3518305,SP,Guararema
3518404,SP,Guaratinguetá
3518503,SP,Guareí
3518602,SP,Guariba
3518701,SP,Guarujá
3518800,SP,Guarulhos
3518859,SP,Guatapará
3518909,SP,Guzolândia
3519006,SP,Herculândia
3519055,SP,Holambra
3519071,SP,Hortolândia
3519105,SP,Iacanga
3519204,SP,Iacri
3519253,SP,Iaras
3519303,SP,Ibaté
3519402,SP,Ibirá
3519501,SP,Ibirarema
3519600,SP,Ibitinga
3519709,SP,Ibiúna
3519808,SP,Icém
3519907,SP,Iepê
3520004,SP,Igaraçu do Tietê
3520103,SP,Igarapava
3520202,SP,Igaratá
3520301,SP,Iguape
3520426,SP,Ilha Comprida
3520442,SP,Ilha Solteira
3520400,SP,Ilhabela
3520509,SP,Indaiatuba
3520608,SP,Indiana
3520707,SP,Indiaporã
3520806,SP,Inúbia Paulista
3520905,SP,Ipaussu
3521002,SP,Iperó
3521101,SP,Ipeúna
3521150,SP,Ipiguá
3521200,SP,Iporanga
3521309,SP,Ipuã
3521408,SP,Iracemápolis
3521507,SP,Irapuã
3521606,SP,Irapuru
3521705,SP,Itaberá
3521804,SP,Itaí
3521903,SP,Itajobi
3522000,SP,Itaju
3522109,SP,Itanhaém
3522158,SP,Itaoca
3522208,SP,Itapecerica da Serra
3522307,SP,Itapetininga
3522406,SP,Itapeva
3522505,SP,Itapevi
3522604,SP,Itapira
3522653,SP,Itapirapuã Paulista
3522703,SP,Itápolis
3522802,SP,Itaporanga
3522901,SP,Itapuí
3523008,SP,Itapura
3523107,SP,Itaquaquecetuba
3523206,SP,Itararé
3523305,SP,Itariri
3523404,SP,Itatiba
3523503,SP,Itatinga
3523602,SP,Itirapina
3523701,SP,Itirapuã
3523800,SP,Itobi
3523909,SP,Itu
3524006,SP,Itupeva
3524105,SP,Ituverava
3524204,SP,Jaborandi
3524303,SP,Jaboticabal
3524402,SP,Jacareí
3524501,SP,Jaci
3524600,SP,Jacupiranga
3524709,SP,Jaguariúna
3524808,SP,Jales
3524907,SP,Jambeiro
3525003,SP,Jandira
3525102,SP,Jardinópolis
3525201,SP,Jarinu
3525300,SP,Jaú
3525409,SP,Jeriquara
3525508,SP,Joanópolis
3525607,SP,João Ramalho
3525706,SP,José Bonifácio
3525805,SP,Júlio Mesquita
3525854,SP,Jumirim
3525904,SP,Jundiaí
3526001,SP,Junqueirópolis
3526100,SP,Juquiá
3526209,SP,Juquitiba
3526308,SP,Lagoinha
3526407,SP,Laranjal Paulista
3526506,SP,Lavínia
3526605,SP,Lavrinhas
3526704,SP,Leme
3526803,SP,Lençóis Paulista
3526902,SP,Limeira
3527009,SP,Lindóia
3527108,SP,Lins
3527207,SP,Lorena
3527256,SP,Lourdes
3527306,SP,Louveira
3527405,SP,Lucélia
3527504,SP,Lucianópolis
3527603,SP,Luís Antônio
3527702,SP,Luiziânia
3527801,SP,Lupércio
3527900,SP,Lutécia
3528007,SP,Macatuba
3528106,SP,Macaubal
3528205,SP,Macedônia
3528304,SP,Magda
3528403,SP,Mairinque
3528502,SP,Mairiporã
3528601,SP,Manduri
3528700,SP,Marabá Paulista
3528809,SP,Maracaí
3528858,SP,Marapoama
3528908,SP,Mariápolis
3529005,SP,Marília
3529104,SP,Marinópolis
3529203,SP,Martinópolis
3529302,SP,Matão
3529401,SP,Mauá
3529500,SP,Mendonça
3529609,SP,Meridiano
3529658,SP,Mesópolis
3529708,SP,Miguelópolis
3529807,SP,Mineiros do Tietê
3530003,SP,Mira Estrela
3529906,SP,Miracatu
3530102,SP,Mirandópolis
3530201,SP,Mirante do Paranapanema
3530300,SP,Mirassol
3530409,SP,Mirassolândia
3530508,SP,Mococa
3530607,SP,Mogi das Cruzes
3530706,SP,Mogi Guaçu
3530805,SP,Mogi Mirim
3530904,SP,Mombuca
3531001,SP,Monções
3531100,SP,Mongaguá
3531209,SP,Monte Alegre do Sul
3531308,SP,Monte Alto
3531407,SP,Monte Aprazível
3531506,SP,Monte Azul Paulista
3531605,SP,Monte Castelo
3531803,SP,Monte Mor
3531704,SP,Monteiro Lobato
3531902,SP,Morro Agudo
3532009,SP,Morungaba
3532058,SP,Motuca
3532108,SP,Murutinga do Sul
3532157,SP,Nantes
3532207,SP,Narandiba
3532306,SP,Natividade da Serra
3532405,SP,Nazaré Paulista
3532504,SP,Neves Paulista
3532603,SP,Nhandeara
3532702,SP,Nipoã
3532801,SP,Nova Aliança
3532827,SP,Nova Campina
3532843,SP,Nova Canaã Paulista
3532868,SP,Nova Castilho
3532900,SP,Nova Europa
3533007,SP,Nova Granada
3533106,SP,Nova Guataporanga
3533205,SP,Nova Independência
3533304,SP,Nova Luzitânia
3533403,SP,Nova Odessa
3533254,SP,Novais
3533502,SP,Novo Horizonte
3533601,SP,Nuporanga
3533700,SP,Ocauçu
3533809,SP,Óleo
3533908,SP,Olímpia
3534005,SP,Onda Verde
3534104,SP,Oriente
3534203,SP,Orindiúva
3534302,SP,Orlândia
3534401,SP,Osasco
3534500,SP,Oscar Bressane
3534609,SP,Osvaldo Cruz
3534708,SP,Ourinhos
3534807,SP,Ouro Verde
3534757,SP,Ouroeste
3534906,SP,Pacaembu
3535002,SP,Palestina
3535101,SP,Palmares Paulista
3535200,SP,Palmeira Oeste
3535309,SP,Palmital
3535408,SP,Panorama
3535507,SP,Paraguaçu Paulista
3535606,SP,Paraibuna
3535705,SP,Paraíso
3535804,SP,Paranapanema
3535903,SP,Paranapuã
3536000,SP,Parapuã
3536109,SP,Pardinho
3536208,SP,Pariquera-Açu
3536257,SP,Parisi
3536307,SP,Patrocínio Paulista
3536406,SP,Paulicéia
3536505,SP,Paulínia
3536570,SP,Paulistânia
3536604,SP,Paulo de Faria
3536703,SP,Pederneiras
3536802,SP,Pedra Bela
3536901,SP,Pedranópolis
3537008,SP,Pedregulho
3537107,SP,Pedreira
3537156,SP,Pedrinhas Paulista
3537206,SP,Pedro de Toledo
3537305,SP,Penápolis
3537404,SP,Pereira Barreto
3537503,SP,Pereiras
3537602,SP,Peruíbe
3537701,SP,Piacatu
3537800,SP,Piedade
3537909,SP,Pilar do Sul
3538006,SP,Pindamonhangaba
3538105,SP,Pindorama
3538204,SP,Pinhalzinho
3538303,SP,Piquerobi
3538501,SP,Piquete
3538600,SP,Piracaia
3538709,SP,Piracicaba
3538808,SP,Piraju
3538907,SP,Pirajuí
3539004,SP,Pirangi
3539103,SP,Pirapora do Bom Jesus
3539202,SP,Pirapozinho
3539301,SP,Pirassununga
3539400,SP,Piratininga
3539509,SP,Pitangueiras
3539608,SP,Planalto
3539707,SP,Platina
3539806,SP,Poá
3539905,SP,Poloni
3540002,SP,Pompéia
3540101,SP,Pongaí
3540200,SP,Pontal
3540259,SP,Pontalinda
3540309,SP,Pontes Gestal
3540408,SP,Populina
3540507,SP,Porangaba
3540606,SP,Porto Feliz
3540705,SP,Porto Ferreira
3540754,SP,Potim
3540804,SP,Potirendaba
3540853,SP,Pracinha
3540903,SP,Pradópolis
3541000,SP,Praia Grande
3541059,SP,Pratânia
3541109,SP,Presidente Alves
3541208,SP,Presidente Bernardes
3541307,SP,Presidente Epitácio
3541406,SP,Presidente Prudente
3541505,SP,Presidente Venceslau
3541604,SP,Promissão
3541653,SP,Quadra
3541703,SP,Quatá
3541802,SP,Queiroz
3541901,SP,Queluz
3542008,SP,Quintana
3542107,SP,Rafard
3542206,SP,Rancharia
3542305,SP,Redenção da Serra
3542404,SP,Regente Feijó
3542503,SP,Reginópolis
3542602,SP,Registro
3542701,SP,Restinga
3542800,SP,Ribeira
3542909,SP,Ribeirão Bonito
3543006,SP,Ribeirão Branco
3543105,SP,Ribeirão Corrente
3543204,SP,Ribeirão do Sul
3543238,SP,Ribeirão dos Índios
3543253,SP,Ribeirão Grande
3543303,SP,Ribeirão Pires
3543402,SP,Ribeirão Preto
3543600,SP,Rifaina
3543709,SP,Rincão
3543808,SP,Rinópolis
3543907,SP,Rio Claro
3544004,SP,Rio das Pedras
3544103,SP,Rio Grande da Serra
3544202,SP,Riolândia
3543501,SP,Riversul
3544251,SP,Rosana
3544301,SP,Roseira
3544400,SP,Rubiácea
3544509,SP,Rubinéia
3544608,SP,Sabino
3544707,SP,Sagres
3544806,SP,Sales
3544905,SP,Sales Oliveira
3545001,SP,Salesópolis
3545100,SP,Salmourão
3545159,SP,Saltinho
3545209,SP,Salto
3545308,SP,Salto de Pirapora
3545407,SP,Salto Grande
3545506,SP,Sandovalina
3545605,SP,Santa Adélia
3545704,SP,Santa Albertina
3545803,SP,Santa Bárbara Oeste
3546009,SP,Santa Branca
3546108,SP,Santa Clara Oeste
3546207,SP,Santa Cruz da Conceição
3546256,SP,Santa Cruz da Esperança
3546306,SP,Santa Cruz das Palmeiras
3546405,SP,Santa Cruz do Rio Pardo
3546504,SP,Santa Ernestina
3546603,SP,Santa Fé do Sul
3546702,SP,Santa Gertrudes
3546801,SP,Santa Isabel
3546900,SP,Santa Lúcia
3547007,SP,Santa Maria da Serra
3547106,SP,Santa Mercedes
3547502,SP,Santa Rita do Passa Quatro
3547403,SP,Santa Rita Oeste
3547601,SP,Santa Rosa de Viterbo
3547650,SP,Santa Salete
3547205,SP,Santana da Ponte Pensa
3547304,SP,Santana de Parnaíba
3547700,SP,Santo Anastácio
3547809,SP,Santo André
3547908,SP,Santo Antônio da Alegria
3548005,SP,Santo Antônio de Posse
3548054,SP,Santo Antônio do Aracanguá
3548104,SP,Santo Antônio do Jardim
3548203,SP,Santo Antônio do Pinhal
3548302,SP,Santo Expedito
3548401,SP,Santópolis do Aguapeí
3548500,SP,Santos
3548609,SP,São Bento do Sapucaí
3548708,SP,São Bernardo do Campo
3548807,SP,São Caetano do Sul
3548906,SP,São Carlos
3549003,SP,São Francisco
3549102,SP,São João da Boa Vista
3549201,SP,São João das Duas Pontes
3549250,SP,São João de Iracema
3549300,SP,São João do Pau Alho
3549409,SP,São Joaquim da Barra
3549508,SP,São José da Bela Vista
3549607,SP,São José do Barreiro
3549706,SP,São José do Rio Pardo
3549805,SP,São José do Rio Preto
3549904,SP,São José dos Campos
3549953,SP,São Lourenço da Serra
3550001,SP,São Luiz do Paraitinga
3550100,SP,São Manuel
3550209,SP,São Miguel Arcanjo
3550308,SP,São Paulo
3550407,SP,São Pedro
3550506,SP,São Pedro do Turvo
3550605,SP,São Roque
3550704,SP,São Sebastião
3550803,SP,São Sebastião da Grama
3550902,SP,São Simão
3551009,SP,São Vicente
3551108,SP,Sarapuí
3551207,SP,Sarutaiá
3551306,SP,Sebastianópolis do Sul
3551405,SP,Serra Azul
3551603,SP,Serra Negra
3551504,SP,Serrana
3551702,SP,Sertãozinho
3551801,SP,Sete Barras
3551900,SP,Severínia
3552007,SP,Silveiras
3552106,SP,Socorro
3552205,SP,Sorocaba
3552304,SP,Sud Mennucci
3552403,SP,Sumaré
3552551,SP,Suzanápolis
3552502,SP,Suzano
3552601,SP,Tabapuã
3552700,SP,Tabatinga
3552809,SP,Taboão da Serra
3552908,SP,Taciba
3553005,SP,Taguaí
3553104,SP,Taiaçu
3553203,SP,Taiúva
3553302,SP,Tambaú
3553401,SP,Tanabi
3553500,SP,Tapiraí
3553609,SP,Tapiratiba
3553658,SP,Taquaral
3553708,SP,Taquaritinga
3553807,SP,Taquarituba
3553856,SP,Taquarivaí
3553906,SP,Tarabai
3553955,SP,Tarumã
3554003,SP,Tatuí
3554102,SP,Taubaté
3554201,SP,Tejupá
3554300,SP,Teodoro Sampaio
3554409,SP,Terra Roxa
3554508,SP,Tietê
3554607,SP,Timburi
3554656,SP,Torre de Pedra
3554706,SP,Torrinha
3554755,SP,Trabiju
3554805,SP,Tremembé
3554904,SP,Três Fronteiras
3554953,SP,Tuiuti
3555000,SP,Tupã
3555109,SP,Tupi Paulista
3555208,SP,Turiúba
3555307,SP,Turmalina
3555356,SP,Ubarana
3555406,SP,Ubatuba
3555505,SP,Ubirajara
3555604,SP,Uchoa
3555703,SP,União Paulista
3555802,SP,Urânia
3555901,SP,Uru
3556008,SP,Urupês
3556107,SP,Valentim Gentil
3556206,SP,Valinhos
3556305,SP,Valparaíso
3556354,SP,Vargem
3556404,SP,Vargem Grande do Sul
3556453,SP,Vargem Grande Paulista
3556503,SP,Várzea Paulista
3556602,SP,Vera Cruz
3556701,SP,Vinhedo
3556800,SP,Viradouro
3556909,SP,Vista Alegre do Alto
3556958,SP,Vitória Brasil
3557006,SP,Votorantim
3557105,SP,Votuporanga
3557154,SP,Zacarias
4100103,PR,Abatiá
4100202,PR,Adrianópolis
4100301,PR,Agudos do Sul
4100400,PR,Almirante Tamandaré
4100459,PR,Altamira do Paraná
4128625,PR,Alto Paraíso
4100608,PR,Alto Paraná
4100707,PR,Alto Piquiri
4100509,PR,Altônia
4100806,PR,Alvorada do Sul
4100905,PR,Amaporã
4101002,PR,Ampére
4101051,PR,Anahy
4101101,PR,Andirá
4101150,PR,Ângulo
4101200,PR,Antonina
4101309,PR,Antônio Olinto
4101408,PR,Apucarana
4101507,PR,Arapongas
4101606,PR,Arapoti
4101655,PR,Arapuã
4101705,PR,Araruna
4101804,PR,Araucária
4101853,PR,Ariranha do Ivaí
4101903,PR,Assaí
4102000,PR,Assis Chateaubriand
4102109,PR,Astorga
4102208,PR,Atalaia
4102307,PR,Balsa Nova
4102406,PR,Bandeirantes
4102505,PR,Barbosa Ferraz
4102703,PR,Barra do Jacaré
4102604,PR,Barracão
4102752,PR,Bela Vista da Caroba
4102802,PR,Bela Vista do Paraíso
4102901,PR,Bituruna
4103008,PR,Boa Esperança
4103024,PR,Boa Esperança do Iguaçu
4103040,PR,Boa Ventura de São Roque
4103057,PR,Boa Vista da Aparecida
4103107,PR,Bocaiúva do Sul
4103156,PR,Bom Jesus do Sul
4103206,PR,Bom Sucesso
4103222,PR,Bom Sucesso do Sul
4103305,PR,Borrazópolis
4103354,PR,Braganey
4103370,PR,Brasilândia do Sul
4103404,PR,Cafeara
4103453,PR,Cafelândia
4103479,PR,Cafezal do Sul
4103503,PR,Califórnia
4103602,PR,Cambará
4103701,PR,Cambé
4103800,PR,Cambira
4103909,PR,Campina da Lagoa
4103958,PR,Campina do Simão
4104006,PR,Campina Grande do Sul
4104055,PR,Campo Bonito
4104105,PR,Campo do Tenente
4104204,PR,Campo Largo
4104253,PR,Campo Magro
4104303,PR,Campo Mourão
4104402,PR,Cândido de Abreu
4104428,PR,Candói
4104451,PR,Cantagalo
4104501,PR,Capanema
4104600,PR,Capitão Leônidas Marques
4104659,PR,Carambeí
4104709,PR,Carlópolis
4104808,PR,Cascavel
4104907,PR,Castro
4105003,PR,Catanduvas
4105102,PR,Centenário do Sul
4105201,PR,Cerro Azul
4105300,PR,Céu Azul
4105409,PR,Chopinzinho
4105508,PR,Cianorte
4105607,PR,Cidade Gaúcha
4105706,PR,Clevelândia
4105805,PR,Colombo
4105904,PR,Colorado
4106001,PR,Congonhinhas
4106100,PR,Conselheiro Mairinck
4106209,PR,Contenda
4106308,PR,Corbélia
4106407,PR,Cornélio Procópio
4106456,PR,Coronel Domingos Soares
4106506,PR,Coronel Vivida
4106555,PR,Corumbataí do Sul
4106803,PR,Cruz Machado
4106571,PR,Cruzeiro do Iguaçu
4106605,PR,Cruzeiro do Oeste
4106704,PR,Cruzeiro do Sul
4106852,PR,Cruzmaltina
4106902,PR,Curitiba
4107009,PR,Curiúva
4107108,PR,Diamante do Norte
4107124,PR,Diamante do Sul
4107157,PR,Diamante Oeste
4107207,PR,Dois Vizinhos
4107256,PR,Douradina
4107306,PR,Doutor Camargo
4128633,PR,Doutor Ulysses
4107405,PR,Enéas Marques
4107504,PR,Engenheiro Beltrão
4107538,PR,Entre Rios do Oeste
4107520,PR,Esperança Nova
4107546,PR,Espigão Alto do Iguaçu
4107553,PR,Farol
4107603,PR,Faxinal
4107652,PR,Fazenda Rio Grande
4107702,PR,Fênix
4107736,PR,Fernandes Pinheiro
4107751,PR,Figueira
4107850,PR,Flor da Serra do Sul
4107801,PR,Floraí
4107900,PR,Floresta
4108007,PR,Florestópolis
4108106,PR,Flórida
4108205,PR,Formosa do Oeste
4108304,PR,Foz do Iguaçu
4108452,PR,Foz do Jordão
4108320,PR,Francisco Alves
4108403,PR,Francisco Beltrão
4108502,PR,General Carneiro
4108551,PR,Godoy Moreira
4108601,PR,Goioerê
4108650,PR,Goioxim
4108700,PR,Grandes Rios
4108809,PR,Guaíra
4108908,PR,Guairaçá
4108957,PR,Guamiranga
4109005,PR,Guapirama
4109104,PR,Guaporema
4109203,PR,Guaraci
4109302,PR,Guaraniaçu
4109401,PR,Guarapuava
4109500,PR,Guaraqueçaba
4109609,PR,Guaratuba
4109658,PR,Honório Serpa
4109708,PR,Ibaiti
4109757,PR,Ibema
4109807,PR,Ibiporã
4109906,PR,Icaraíma
4110003,PR,Iguaraçu
4110052,PR,Iguatu
4110078,PR,Imbaú
4110102,PR,Imbituva
4110201,PR,Inácio Martins
4110300,PR,Inajá
4110409,PR,Indianópolis
4110508,PR,Ipiranga
4110607,PR,Iporã
4110656,PR,Iracema do Oeste
4110706,PR,Irati
4110805,PR,Iretama
4110904,PR,Itaguajé
4110953,PR,Itaipulândia
4111001,PR,Itambaracá
4111100,PR,Itambé
4111209,PR,Itapejara Oeste
4111258,PR,Itaperuçu
4111308,PR,Itaúna do Sul
4111407,PR,Ivaí
4111506,PR,Ivaiporã
4111555,PR,Ivaté
4111605,PR,Ivatuba
4111704,PR,Jaboti
4111803,PR,Jacarezinho
4111902,PR,Jaguapitã
4112009,PR,Jaguariaíva
4112108,PR,Jandaia do Sul
4112207,PR,Janiópolis
4112306,PR,Japira
4112405,PR,Japurá
4112504,PR,Jardim Alegre
4112603,PR,Jardim Olinda
4112702,PR,Jataizinho
4112751,PR,Jesuítas
4112801,PR,Joaquim Távora
4112900,PR,Jundiaí do Sul
4112959,PR,Juranda
4113007,PR,Jussara
4113106,PR,Kaloré
4113205,PR,Lapa
4113254,PR,Laranjal
4113304,PR,Laranjeiras do Sul
4113403,PR,Leópolis
4113429,PR,Lidianópolis
4113452,PR,Lindoeste
4113502,PR,Loanda
4113601,PR,Lobato
4113700,PR,Londrina
4113734,PR,Luiziana
4113759,PR,Lunardelli
4113809,PR,Lupionópolis
4113908,PR,Mallet
4114005,PR,Mamborê
4114104,PR,Mandaguaçu
4114203,PR,Mandaguari
4114302,PR,Mandirituba
4114351,PR,Manfrinópolis
4114401,PR,Mangueirinha
4114500,PR,Manoel Ribas
4114609,PR,Marechal Cândido Rondon
4114708,PR,Maria Helena
4114807,PR,Marialva
4114906,PR,Marilândia do Sul
4115002,PR,Marilena
4115101,PR,Mariluz
4115200,PR,Maringá
4115309,PR,Mariópolis
4115358,PR,Maripá
4115408,PR,Marmeleiro
4115457,PR,Marquinho
4115507,PR,Marumbi
4115606,PR,Matelândia
4115705,PR,Matinhos
4115739,PR,Mato Rico
4115754,PR,Mauá da Serra
4115804,PR,Medianeira
4115853,PR,Mercedes
4115903,PR,Mirador
4116000,PR,Miraselva
4116059,PR,Missal
4116109,PR,Moreira Sales
4116208,PR,Morretes
4116307,PR,Munhoz de Melo
4116406,PR,Nossa Senhora das Graças
4116505,PR,Nova Aliança do Ivaí
4116604,PR,Nova América da Colina
4116703,PR,Nova Aurora
4116802,PR,Nova Cantu
4116901,PR,Nova Esperança
4116950,PR,Nova Esperança do Sudoeste
4117008,PR,Nova Fátima
4117057,PR,Nova Laranjeiras
4117107,PR,Nova Londrina
4117206,PR,Nova Olímpia
4117255,PR,Nova Prata do Iguaçu
4117214,PR,Nova Santa Bárbara
4117222,PR,Nova Santa Rosa
4117271,PR,Nova Tebas
4117297,PR,Novo Itacolomi
4117305,PR,Ortigueira
4117404,PR,Ourizona
4117453,PR,Ouro Verde do Oeste
4117503,PR,Paiçandu
4117602,PR,Palmas
4117701,PR,Palmeira
4117800,PR,Palmital
4117909,PR,Palotina
4118006,PR,Paraíso do Norte
4118105,PR,Paranacity
4118204,PR,Paranaguá
4118303,PR,Paranapoema
4118402,PR,Paranavaí
4118451,PR,Pato Bragado
4118501,PR,Pato Branco
4118600,PR,Paula Freitas
4118709,PR,Paulo Frontin
4118808,PR,Peabiru
4118857,PR,Perobal
4118907,PR,Pérola
4119004,PR,Pérola Oeste
4119103,PR,Piên
4119152,PR,Pinhais
4119251,PR,Pinhal de São Bento
4119202,PR,Pinhalão
4119301,PR,Pinhão
4119400,PR,Piraí do Sul
4119509,PR,Piraquara
4119608,PR,Pitanga
4119657,PR,Pitangueiras
4119707,PR,Planaltina do Paraná
4119806,PR,Planalto
4119905,PR,Ponta Grossa
4119954,PR,Pontal do Paraná
4120002,PR,Porecatu
4120101,PR,Porto Amazonas
4120150,PR,Porto Barreiro
4120200,PR,Porto Rico
4120309,PR,Porto Vitória
4120333,PR,Prado Ferreira
4120358,PR,Pranchita
4120408,PR,Presidente Castelo Branco
4120507,PR,Primeiro de Maio
4120606,PR,Prudentópolis
4120655,PR,Quarto Centenário
4120705,PR,Quatiguá
4120804,PR,Quatro Barras
4120853,PR,Quatro Pontes
4120903,PR,Quedas do Iguaçu
4121000,PR,Querência do Norte
4121109,PR,Quinta do Sol
4121208,PR,Quitandinha
4121257,PR,Ramilândia
4121307,PR,Rancho Alegre
4121356,PR,Rancho Alegre Oeste
4121406,PR,Realeza
4121505,PR,Rebouças
4121604,PR,Renascença
4121703,PR,Reserva
4121752,PR,Reserva do Iguaçu
4121802,PR,Ribeirão Claro
4121901,PR,Ribeirão do Pinhal
4122008,PR,Rio Azul
4122107,PR,Rio Bom
4122156,PR,Rio Bonito do Iguaçu
4122172,PR,Rio Branco do Ivaí
4122206,PR,Rio Branco do Sul
4122305,PR,Rio Negro
4122404,PR,Rolândia
4122503,PR,Roncador
4122602,PR,Rondon
4122651,PR,Rosário do Ivaí
4122701,PR,Sabáudia
4122800,PR,Salgado Filho
4122909,PR,Salto do Itararé
4123006,PR,Salto do Lontra
4123105,PR,Santa Amélia
4123204,PR,Santa Cecília do Pavão
4123303,PR,Santa Cruz de Monte Castelo
4123402,PR,Santa Fé
4123501,PR,Santa Helena
4123600,PR,Santa Inês
4123709,PR,Santa Isabel do Ivaí
4123808,PR,Santa Izabel do Oeste
4123824,PR,Santa Lúcia
4123857,PR,Santa Maria do Oeste
4123907,PR,Santa Mariana
4123956,PR,Santa Mônica
4124020,PR,Santa Tereza do Oeste
4124053,PR,Santa Terezinha de Itaipu
4124004,PR,Santana do Itararé
4124103,PR,Santo Antônio da Platina
4124202,PR,Santo Antônio do Caiuá
4124301,PR,Santo Antônio do Paraíso
4124400,PR,Santo Antônio do Sudoeste
4124509,PR,Santo Inácio
4124608,PR,São Carlos do Ivaí
4124707,PR,São Jerônimo da Serra
4124806,PR,São João
4124905,PR,São João do Caiuá
4125001,PR,São João do Ivaí
4125100,PR,São João do Triunfo
4125308,PR,São Jorge do Ivaí
4125357,PR,São Jorge do Patrocínio
4125209,PR,São Jorge Oeste
4125407,PR,São José da Boa Vista
4125456,PR,São José das Palmeiras
4125506,PR,São José dos Pinhais
4125555,PR,São Manoel do Paraná
4125605,PR,São Mateus do Sul
4125704,PR,São Miguel do Iguaçu
4125753,PR,São Pedro do Iguaçu
4125803,PR,São Pedro do Ivaí
4125902,PR,São Pedro do Paraná
4126009,PR,São Sebastião da Amoreira
4126108,PR,São Tomé
4126207,PR,Sapopema
4126256,PR,Sarandi
4126272,PR,Saudade do Iguaçu
4126306,PR,Sengés
4126355,PR,Serranópolis do Iguaçu
4126405,PR,Sertaneja
4126504,PR,Sertanópolis
4126603,PR,Siqueira Campos
4126652,PR,Sulina
4126678,PR,Tamarana
4126702,PR,Tamboara
4126801,PR,Tapejara
4126900,PR,Tapira
4127007,PR,Teixeira Soares
4127106,PR,Telêmaco Borba
4127205,PR,Terra Boa
4127304,PR,Terra Rica
4127403,PR,Terra Roxa
4127502,PR,Tibagi
4127601,PR,Tijucas do Sul
4127700,PR,Toledo
4127809,PR,Tomazina
4127858,PR,Três Barras do Paraná
4127882,PR,Tunas do Paraná
4127908,PR,Tuneiras do Oeste
4127957,PR,Tupãssi
4127965,PR,Turvo
4128005,PR,Ubiratã
4128104,PR,Umuarama
4128203,PR,União da Vitória
4128302,PR,Uniflor
4128401,PR,Uraí
4128534,PR,Ventania
4128559,PR,Vera Cruz do Oeste
4128609,PR,Verê
4128658,PR,Virmond
4128708,PR,Vitorino
4128500,PR,Wenceslau Braz
4128807,PR,Xambrê
4200051,SC,Abdon Batista
4200101,SC,Abelardo Luz
4200200,SC,Agrolândia
4200309,SC,Agronômica
4200408,SC,Água Doce
4200507,SC,Águas de Chapecó
4200556,SC,Águas Frias
4200606,SC,Águas Mornas
4200705,SC,Alfredo Wagner
4200754,SC,Alto Bela Vista
4200804,SC,Anchieta
4200903,SC,Angelina
4201000,SC,Anita Garibaldi
4201109,SC,Anitápolis
4201208,SC,Antônio Carlos
4201257,SC,Apiúna
4201273,SC,Arabutã
4201307,SC,Araquari
4201406,SC,Araranguá
4201505,SC,Armazém
4201604,SC,Arroio Trinta
4201653,SC,Arvoredo
4201703,SC,Ascurra
4201802,SC,Atalanta
4201901,SC,Aurora
4201950,SC,Balneário Arroio do Silva
4202057,SC,Balneário Barra do Sul
4202008,SC,Balneário Camboriú
4202073,SC,Balneário Gaivota
4212809,SC,Balneário Piçarras
4220000,SC,Balneário Rincão
4202081,SC,Bandeirante
4202099,SC,Barra Bonita
4202107,SC,Barra Velha
4202131,SC,Bela Vista do Toldo
4202156,SC,Belmonte
4202206,SC,Benedito Novo
4202305,SC,Biguaçu
4202404,SC,Blumenau
4202438,SC,Bocaina do Sul
4202503,SC,Bom Jardim da Serra
4202537,SC,Bom Jesus
4202578,SC,Bom Jesus do Oeste
4202602,SC,Bom Retiro
4202453,SC,Bombinhas
4202701,SC,Botuverá
4202800,SC,Braço do Norte
4202859,SC,Braço do Trombudo
4202875,SC,Brunópolis
4202909,SC,Brusque
4203006,SC,Caçador
4203105,SC,Caibi
4203154,SC,Calmon
4203204,SC,Camboriú
4203303,SC,Campo Alegre
4203402,SC,Campo Belo do Sul
4203501,SC,Campo Erê
4203600,SC,Campos Novos
4203709,SC,Canelinha
4203808,SC,Canoinhas
4203253,SC,Capão Alto
4203907,SC,Capinzal
4203956,SC,Capivari de Baixo
4204004,SC,Catanduvas
4204103,SC,Caxambu do Sul
4204152,SC,Celso Ramos
4204178,SC,Cerro Negro
4204194,SC,Chapadão do Lageado
4204202,SC,Chapecó
4204251,SC,Cocal do Sul
4204301,SC,Concórdia
4204350,SC,Cordilheira Alta
4204400,SC,Coronel Freitas
4204459,SC,Coronel Martins
4204558,SC,Correia Pinto
4204509,SC,Corupá
4204608,SC,Criciúma
4204707,SC,Cunha Porã
4204756,SC,Cunhataí
4204806,SC,Curitibanos
4204905,SC,Descanso
4205001,SC,Dionísio Cerqueira
4205100,SC,Dona Emma
4205159,SC,Doutor Pedrinho
4205175,SC,Entre Rios
4205191,SC,Ermo
4205209,SC,Erval Velho
4205308,SC,Faxinal dos Guedes
4205357,SC,Flor do Sertão
4205407,SC,Florianópolis
4205431,SC,Formosa do Sul
4205456,SC,Forquilhinha
4205506,SC,Fraiburgo
4205555,SC,Frei Rogério
4205605,SC,Galvão
4205704,SC,Garopaba
4205803,SC,Garuva
4205902,SC,Gaspar
4206009,SC,Governador Celso Ramos
4206108,SC,Grão Pará
4206207,SC,Gravatal
4206306,SC,Guabiruba
4206405,SC,Guaraciaba
4206504,SC,Guaramirim
4206603,SC,Guarujá do Sul
4206652,SC,Guatambú
4206702,SC,Herval Oeste
4206751,SC,Ibiam
4206801,SC,Ibicaré
4206900,SC,Ibirama
4207007,SC,Içara
4207106,SC,Ilhota
4207205,SC,Imaruí
4207304,SC,Imbituba
4207403,SC,Imbuia
4207502,SC,Indaial
4207577,SC,Iomerê
4207601,SC,Ipira
4207650,SC,Iporã do Oeste
4207684,SC,Ipuaçu
4207700,SC,Ipumirim
4207759,SC,Iraceminha
4207809,SC,Irani
4207858,SC,Irati
4207908,SC,Irineópolis
4208005,SC,Itá
4208104,SC,Itaiópolis
4208203,SC,Itajaí
4208302,SC,Itapema
4208401,SC,Itapiranga
4208450,SC,Itapoá
4208500,SC,Ituporanga
4208609,SC,Jaborá
4208708,SC,Jacinto Machado
4208807,SC,Jaguaruna
4208906,SC,Jaraguá do Sul
4208955,SC,Jardinópolis
4209003,SC,Joaçaba
4209102,SC,Joinville
4209151,SC,José Boiteux
4209177,SC,Jupiá
4209201,SC,Lacerdópolis
4209300,SC,Lages
4209409,SC,Laguna
4209458,SC,Lajeado Grande
4209508,SC,Laurentino
4209607,SC,Lauro Müller
4209706,SC,Lebon Régis
4209805,SC,Leoberto Leal
4209854,SC,Lindóia do Sul
4209904,SC,Lontras
4210001,SC,Luiz Alves
4210035,SC,Luzerna
4210050,SC,Macieira
4210100,SC,Mafra
4210209,SC,Major Gercino
4210308,SC,Major Vieira
4210407,SC,Maracajá
4210506,SC,Maravilha
4210555,SC,Marema
4210605,SC,Massaranduba
4210704,SC,Matos Costa
4210803,SC,Meleiro
4210852,SC,Mirim Doce
4210902,SC,Modelo
4211009,SC,Mondaí
4211058,SC,Monte Carlo
4211108,SC,Monte Castelo
4211207,SC,Morro da Fumaça
4211256,SC,Morro Grande
4211306,SC,Navegantes
4211405,SC,Nova Erechim
4211454,SC,Nova Itaberaba
4211504,SC,Nova Trento
4211603,SC,Nova Veneza
4211652,SC,Novo Horizonte
4211702,SC,Orleans
4211751,SC,Otacílio Costa
4211801,SC,Ouro
4211850,SC,Ouro Verde
4211876,SC,Paial
4211892,SC,Painel
4211900,SC,Palhoça
4212007,SC,Palma Sola
4212056,SC,Palmeira
4212106,SC,Palmitos
4212205,SC,Papanduva
4212239,SC,Paraíso
4212254,SC,Passo de Torres
4212270,SC,Passos Maia
4212304,SC,Paulo Lopes
4212403,SC,Pedras Grandes
4212502,SC,Penha
4212601,SC,Peritiba
4212650,SC,Pescaria Brava
4212700,SC,Petrolândia
4212908,SC,Pinhalzinho
4213005,SC,Pinheiro Preto
4213104,SC,Piratuba
4213153,SC,Planalto Alegre
4213203,SC,Pomerode
4213302,SC,Ponte Alta
4213351,SC,Ponte Alta do Norte
4213401,SC,Ponte Serrada
4213500,SC,Porto Belo
4213609,SC,Porto União
4213708,SC,Pouso Redondo
4213807,SC,Praia Grande
4213906,SC,Presidente Castello Branco
4214003,SC,Presidente Getúlio
4214102,SC,Presidente Nereu
4214151,SC,Princesa
4214201,SC,Quilombo
4214300,SC,Rancho Queimado
4214409,SC,Rio das Antas
4214508,SC,Rio do Campo
4214607,SC,Rio do Oeste
4214805,SC,Rio do Sul
4214706,SC,Rio dos Cedros
4214904,SC,Rio Fortuna
4215000,SC,Rio Negrinho
4215059,SC,Rio Rufino
4215075,SC,Riqueza
4215109,SC,Rodeio
4215208,SC,Romelândia
4215307,SC,Salete
4215356,SC,Saltinho
4215406,SC,Salto Veloso
4215455,SC,Sangão
4215505,SC,Santa Cecília
4215554,SC,Santa Helena
4215604,SC,Santa Rosa de Lima
4215653,SC,Santa Rosa do Sul
4215679,SC,Santa Terezinha
4215687,SC,Santa Terezinha do Progresso
4215695,SC,Santiago do Sul
4215703,SC,Santo Amaro da Imperatriz
4215802,SC,São Bento do Sul
4215752,SC,São Bernardino
4215901,SC,São Bonifácio
4216008,SC,São Carlos
4216057,SC,São Cristóvão do Sul
4216107,SC,São Domingos
4216206,SC,São Francisco do Sul
4216305,SC,São João Batista
4216354,SC,São João do Itaperiú
4216255,SC,São João do Oeste
4216404,SC,São João do Sul
4216503,SC,São Joaquim
4216602,SC,São José
4216701,SC,São José do Cedro
4216800,SC,São José do Cerrito
4216909,SC,São Lourenço do Oeste
4217006,SC,São Ludgero
4217105,SC,São Martinho
4217154,SC,São Miguel da Boa Vista
4217204,SC,São Miguel do Oeste
4217253,SC,São Pedro de Alcântara
4217303,SC,Saudades
4217402,SC,Schroeder
4217501,SC,Seara
4217550,SC,Serra Alta
4217600,SC,Siderópolis
4217709,SC,Sombrio
4217758,SC,Sul Brasil
4217808,SC,Taió
4217907,SC,Tangará
4217956,SC,Tigrinhos
4218004,SC,Tijucas
4218103,SC,Timbé do Sul
4218202,SC,Timbó
4218251,SC,Timbó Grande
4218301,SC,Três Barras
4218350,SC,Treviso
4218400,SC,Treze de Maio
4218509,SC,Treze Tílias
4218608,SC,Trombudo Central
4218707,SC,Tubarão
4218756,SC,Tunápolis
4218806,SC,Turvo
4218855,SC,União do Oeste
4218905,SC,Urubici
4218954,SC,Urupema
4219002,SC,Urussanga
4219101,SC,Vargeão
4219150,SC,Vargem
4219176,SC,Vargem Bonita
4219200,SC,Vidal Ramos
4219309,SC,Videira
4219358,SC,Vitor Meireles
4219408,SC,Witmarsum
4219507,SC,Xanxerê
4219606,SC,Xavantina
4219705,SC,Xaxim
4219853,SC,Zortéa
4300034,RS,Aceguá
4300059,RS,Água Santa
4300109,RS,Agudo
4300208,RS,Ajuricaba
4300307,RS,Alecrim
4300406,RS,Alegrete
4300455,RS,Alegria
4300471,RS,Almirante Tamandaré do Sul
4300505,RS,Alpestre
4300554,RS,Alto Alegre
4300570,RS,Alto Feliz
4300604,RS,Alvorada
4300638,RS,Amaral Ferrador
4300646,RS,Ametista do Sul
4300661,RS,André da Rocha
4300703,RS,Anta Gorda
4300802,RS,Antônio Prado
4300851,RS,Arambaré
4300877,RS,Araricá
4300901,RS,Aratiba
4301008,RS,Arroio do Meio
4301073,RS,Arroio do Padre
4301057,RS,Arroio do Sal
4301206,RS,Arroio do Tigre
4301107,RS,Arroio dos Ratos
4301305,RS,Arroio Grande
4301404,RS,Arvorezinha
4301503,RS,Augusto Pestana
4301552,RS,Áurea
4301602,RS,Bagé
4301636,RS,Balneário Pinhal
4301651,RS,Barão
4301701,RS,Barão de Cotegipe
4301750,RS,Barão do Triunfo
4301859,RS,Barra do Guarita
4301875,RS,Barra do Quaraí
4301909,RS,Barra do Ribeiro
4301925,RS,Barra do Rio Azul
4301958,RS,Barra Funda
4301800,RS,Barracão
4302006,RS,Barros Cassal
4302055,RS,Benjamin Constant do Sul
4302105,RS,Bento Gonçalves
4302154,RS,Boa Vista das Missões
4302204,RS,Boa Vista do Buricá
4302220,RS,Boa Vista do Cadeado
4302238,RS,Boa Vista do Incra
4302253,RS,Boa Vista do Sul
4302303,RS,Bom Jesus
4302352,RS,Bom Princípio
4302378,RS,Bom Progresso
4302402,RS,Bom Retiro do Sul
4302451,RS,Boqueirão do Leão
4302501,RS,Bossoroca
4302584,RS,Bozano
4302600,RS,Braga
4302659,RS,Brochier
4302709,RS,Butiá
4302808,RS,Caçapava do Sul
4302907,RS,Cacequi
4303004,RS,Cachoeira do Sul
4303103,RS,Cachoeirinha
4303202,RS,Cacique Doble
4303301,RS,Caibaté
4303400,RS,Caiçara
4303509,RS,Camaquã
4303558,RS,Camargo
4303608,RS,Cambará do Sul
4303673,RS,Campestre da Serra
4303707,RS,Campina das Missões
4303806,RS,Campinas do Sul
4303905,RS,Campo Bom
4304002,RS,Campo Novo
4304101,RS,Campos Borges
4304200,RS,Candelária
4304309,RS,Cândido Godói
4304358,RS,Candiota
4304408,RS,Canela
4304507,RS,Canguçu
4304606,RS,Canoas
4304614,RS,Canudos do Vale
4304622,RS,Capão Bonito do Sul
4304630,RS,Capão da Canoa
4304655,RS,Capão do Cipó
4304663,RS,Capão do Leão
4304689,RS,Capela de Santana
4304697,RS,Capitão
4304671,RS,Capivari do Sul
4304713,RS,Caraá
4304705,RS,Carazinho
4304804,RS,Carlos Barbosa
4304853,RS,Carlos Gomes
4304903,RS,Casca
4304952,RS,Caseiros
4305009,RS,Catuípe
4305108,RS,Caxias do Sul
4305116,RS,Centenário
4305124,RS,Cerrito
4305132,RS,Cerro Branco
4305157,RS,Cerro Grande
4305173,RS,Cerro Grande do Sul
4305207,RS,Cerro Largo
4305306,RS,Chapada
4305355,RS,Charqueadas
4305371,RS,Charrua
4305405,RS,Chiapetta
4305439,RS,Chuí
4305447,RS,Chuvisca
4305454,RS,Cidreira
4305504,RS,Ciríaco
4305587,RS,Colinas
4305603,RS,Colorado
4305702,RS,Condor
4305801,RS,Constantina
4305835,RS,Coqueiro Baixo
4305850,RS,Coqueiros do Sul
4305871,RS,Coronel Barros
4305900,RS,Coronel Bicaco
4305934,RS,Coronel Pilar
4305959,RS,Cotiporã
4305975,RS,Coxilha
4306007,RS,Crissiumal
4306056,RS,Cristal
4306072,RS,Cristal do Sul
4306106,RS,Cruz Alta
4306130,RS,Cruzaltense
4306205,RS,Cruzeiro do Sul
4306304,RS,David Canabarro
4306320,RS,Derrubadas
4306353,RS,Dezesseis de Novembro
4306379,RS,Dilermando de Aguiar
4306403,RS,Dois Irmãos
4306429,RS,Dois Irmãos das Missões
4306452,RS,Dois Lajeados
4306502,RS,Dom Feliciano
4306601,RS,Dom Pedrito
4306551,RS,Dom Pedro de Alcântara
4306700,RS,Dona Francisca
4306734,RS,Doutor Maurício Cardoso
4306759,RS,Doutor Ricardo
4306767,RS,Eldorado do Sul
4306809,RS,Encantado
4306908,RS,Encruzilhada do Sul
4306924,RS,Engenho Velho
4306957,RS,Entre Rios do Sul
4306932,RS,Entre-Ijuís
4306973,RS,Erebango
4307005,RS,Erechim
4307054,RS,Ernestina
4307203,RS,Erval Grande
4307302,RS,Erval Seco
4307401,RS,Esmeralda
4307450,RS,Esperança do Sul
4307500,RS,Espumoso
4307559,RS,Estação
4307609,RS,Estância Velha
4307708,RS,Esteio
4307807,RS,Estrela
4307815,RS,Estrela Velha
4307831,RS,Eugênio de Castro
4307864,RS,Fagundes Varela
4307906,RS,Farroupilha
4308003,RS,Faxinal do Soturno
4308052,RS,Faxinalzinho
4308078,RS,Fazenda Vilanova
4308102,RS,Feliz
4308201,RS,Flores da Cunha
4308250,RS,Floriano Peixoto
4308300,RS,Fontoura Xavier
4308409,RS,Formigueiro
4308433,RS,Forquetinha
4308458,RS,Fortaleza dos Valos
4308508,RS,Frederico Westphalen
4308607,RS,Garibaldi
4308656,RS,Garruchos
4308706,RS,Gaurama
4308805,RS,General Câmara
4308854,RS,Gentil
4308904,RS,Getúlio Vargas
4309001,RS,Giruá
4309050,RS,Glorinha
4309100,RS,Gramado
4309126,RS,Gramado dos Loureiros
4309159,RS,Gramado Xavier
4309209,RS,Gravataí
4309258,RS,Guabiju
4309308,RS,Guaíba
4309407,RS,Guaporé
4309506,RS,Guarani das Missões
4309555,RS,Harmonia
4307104,RS,Herval
4309571,RS,Herveiras
4309605,RS,Horizontina
4309654,RS,Hulha Negra
4309704,RS,Humaitá
4309753,RS,Ibarama
4309803,RS,Ibiaçá
4309902,RS,Ibiraiaras
4309951,RS,Ibirapuitã
4310009,RS,Ibirubá
4310108,RS,Igrejinha
4310207,RS,Ijuí
4310306,RS,Ilópolis
4310330,RS,Imbé
4310363,RS,Imigrante
4310405,RS,Independência
4310413,RS,Inhacorá
4310439,RS,Ipê
4310462,RS,Ipiranga do Sul
4310504,RS,Iraí
4310538,RS,Itaara
4310553,RS,Itacurubi
4310579,RS,Itapuca
4310603,RS,Itaqui
4310652,RS,Itati
4310702,RS,Itatiba do Sul
4310751,RS,Ivorá
4310801,RS,Ivoti
4310850,RS,Jaboticaba
4310876,RS,Jacuizinho
4310900,RS,Jacutinga
4311007,RS,Jaguarão
4311106,RS,Jaguari
4311122,RS,Jaquirana
4311130,RS,Jari
4311155,RS,Jóia
4311205,RS,Júlio de Castilhos
4311239,RS,Lagoa Bonita do Sul
4311270,RS,Lagoa dos Três Cantos
4311304,RS,Lagoa Vermelha
4311254,RS,Lagoão
4311403,RS,Lajeado
4311429,RS,Lajeado do Bugre
4311502,RS,Lavras do Sul
4311601,RS,Liberato Salzano
4311627,RS,Lindolfo Collor
4311643,RS,Linha Nova
4311718,RS,Maçambará
4311700,RS,Machadinho
4311734,RS,Mampituba
4311759,RS,Manoel Viana
4311775,RS,Maquiné
4311791,RS,Maratá
4311809,RS,Marau
4311908,RS,Marcelino Ramos
4311981,RS,Mariana Pimentel
4312005,RS,Mariano Moro
4312054,RS,Marques de Souza
4312104,RS,Mata
4312138,RS,Mato Castelhano
4312153,RS,Mato Leitão
4312179,RS,Mato Queimado
4312203,RS,Maximiliano de Almeida
4312252,RS,Minas do Leão
4312302,RS,Miraguaí
4312351,RS,Montauri
4312377,RS,Monte Alegre dos Campos
4312385,RS,Monte Belo do Sul
4312401,RS,Montenegro
4312427,RS,Mormaço
4312443,RS,Morrinhos do Sul
4312450,RS,Morro Redondo
4312476,RS,Morro Reuter
4312500,RS,Mostardas
4312609,RS,Muçum
4312617,RS,Muitos Capões
4312625,RS,Muliterno
4312658,RS,Não-Me-Toque
4312674,RS,Nicolau Vergueiro
4312708,RS,Nonoai
4312757,RS,Nova Alvorada
4312807,RS,Nova Araçá
4312906,RS,Nova Bassano
4312955,RS,Nova Boa Vista
4313003,RS,Nova Bréscia
4313011,RS,Nova Candelária
4313037,RS,Nova Esperança do Sul
4313060,RS,Nova Hartz
4313086,RS,Nova Pádua
4313102,RS,Nova Palma
4313201,RS,Nova Petrópolis
4313300,RS,Nova Prata
4313334,RS,Nova Ramada
4313359,RS,Nova Roma do Sul
4313375,RS,Nova Santa Rita
4313490,RS,Novo Barreiro
4313391,RS,Novo Cabrais
4313409,RS,Novo Hamburgo
4313425,RS,Novo Machado
4313441,RS,Novo Tiradentes
4313466,RS,Novo Xingu
4313508,RS,Osório
4313607,RS,Paim Filho
4313656,RS,Palmares do Sul
4313706,RS,Palmeira das Missões
4313805,RS,Palmitinho
4313904,RS,Panambi
4313953,RS,Pantano Grande
4314001,RS,Paraí
4314027,RS,Paraíso do Sul
4314035,RS,Pareci Novo
4314050,RS,Parobé
4314068,RS,Passa Sete
4314076,RS,Passo do Sobrado
4314100,RS,Passo Fundo
4314134,RS,Paulo Bento
4314159,RS,Paverama
4314175,RS,Pedras Altas
4314209,RS,Pedro Osório
4314308,RS,Pejuçara
4314407,RS,Pelotas
4314423,RS,Picada Café
4314456,RS,Pinhal
4314464,RS,Pinhal da Serra
4314472,RS,Pinhal Grande
4314498,RS,Pinheirinho do Vale
4314506,RS,Pinheiro Machado
4314548,RS,Pinto Bandeira
4314555,RS,Pirapó
4314605,RS,Piratini
4314704,RS,Planalto
4314753,RS,Poço das Antas
4314779,RS,Pontão
4314787,RS,Ponte Preta
4314803,RS,Portão
4314902,RS,Porto Alegre
4315008,RS,Porto Lucena
4315057,RS,Porto Mauá
4315073,RS,Porto Vera Cruz
4315107,RS,Porto Xavier
4315131,RS,Pouso Novo
4315149,RS,Presidente Lucena
4315156,RS,Progresso
4315172,RS,Protásio Alves
4315206,RS,Putinga
4315305,RS,Quaraí
4315313,RS,Quatro Irmãos
4315321,RS,Quevedos
4315354,RS,Quinze de Novembro
4315404,RS,Redentora
4315453,RS,Relvado
4315503,RS,Restinga Sêca
4315552,RS,Rio dos Índios
4315602,RS,Rio Grande
4315701,RS,Rio Pardo
4315750,RS,Riozinho
4315800,RS,Roca Sales
4315909,RS,Rodeio Bonito
4315958,RS,Rolador
4316006,RS,Rolante
4316105,RS,Ronda Alta
4316204,RS,Rondinha
4316303,RS,Roque Gonzales
4316402,RS,Rosário do Sul
4316428,RS,Sagrada Família
4316436,RS,Saldanha Marinho
4316451,RS,Salto do Jacuí
4316477,RS,Salvador das Missões
4316501,RS,Salvador do Sul
4316600,RS,Sananduva
4316709,RS,Santa Bárbara do Sul
4316733,RS,Santa Cecília do Sul
4316758,RS,Santa Clara do Sul
4316808,RS,Santa Cruz do Sul
4316972,RS,Santa Margarida do Sul
4316907,RS,Santa Maria
4316956,RS,Santa Maria do Herval
4317202,RS,Santa Rosa
4317251,RS,Santa Tereza
4317301,RS,Santa Vitória do Palmar
4317004,RS,Santana da Boa Vista
4317103,RS,SantAna do Livramento
4317400,RS,Santiago
4317509,RS,Santo Ângelo
4317608,RS,Santo Antônio da Patrulha
4317707,RS,Santo Antônio das Missões
4317558,RS,Santo Antônio do Palma
4317756,RS,Santo Antônio do Planalto
4317806,RS,Santo Augusto
4317905,RS,Santo Cristo
4317954,RS,Santo Expedito do Sul
4318002,RS,São Borja
4318051,RS,São Domingos do Sul
4318101,RS,São Francisco de Assis
4318200,RS,São Francisco de Paula
4318309,RS,São Gabriel
4318408,RS,São Jerônimo
4318424,RS,São João da Urtiga
4318432,RS,São João do Polêsine
4318440,RS,São Jorge
4318457,RS,São José das Missões
4318465,RS,São José do Herval
4318481,RS,São José do Hortêncio
4318499,RS,São José do Inhacorá
4318507,RS,São José do Norte
4318606,RS,São José do Ouro
4318614,RS,São José do Sul
4318622,RS,São José dos Ausentes
4318705,RS,São Leopoldo
4318804,RS,São Lourenço do Sul
4318903,RS,São Luiz Gonzaga
4319000,RS,São Marcos
4319109,RS,São Martinho
4319125,RS,São Martinho da Serra
4319158,RS,São Miguel das Missões
4319208,RS,São Nicolau
4319307,RS,São Paulo das Missões
4319356,RS,São Pedro da Serra
4319364,RS,São Pedro das Missões
4319372,RS,São Pedro do Butiá
4319406,RS,São Pedro do Sul
4319505,RS,São Sebastião do Caí
4319604,RS,São Sepé
4319703,RS,São Valentim
4319711,RS,São Valentim do Sul
4319737,RS,São Valério do Sul
4319752,RS,São Vendelino
4319802,RS,São Vicente do Sul
4319901,RS,Sapiranga
4320008,RS,Sapucaia do Sul
4320107,RS,Sarandi
4320206,RS,Seberi
4320230,RS,Sede Nova
4320263,RS,Segredo
4320305,RS,Selbach
4320321,RS,Senador Salgado Filho
4320354,RS,Sentinela do Sul
4320404,RS,Serafina Corrêa
4320453,RS,Sério
4320503,RS,Sertão
4320552,RS,Sertão Santana
4320578,RS,Sete de Setembro
4320602,RS,Severiano de Almeida
4320651,RS,Silveira Martins
4320677,RS,Sinimbu
4320701,RS,Sobradinho
4320800,RS,Soledade
4320859,RS,Tabaí
4320909,RS,Tapejara
4321006,RS,Tapera
4321105,RS,Tapes
4321204,RS,Taquara
4321303,RS,Taquari
4321329,RS,Taquaruçu do Sul
4321352,RS,Tavares
4321402,RS,Tenente Portela
4321436,RS,Terra de Areia
4321451,RS,Teutônia
4321469,RS,Tio Hugo
4321477,RS,Tiradentes do Sul
4321493,RS,Toropi
4321501,RS,Torres
4321600,RS,Tramandaí
4321626,RS,Travesseiro
4321634,RS,Três Arroios
4321667,RS,Três Cachoeiras
4321709,RS,Três Coroas
4321808,RS,Três de Maio
4321832,RS,Três Forquilhas
4321857,RS,Três Palmeiras
4321907,RS,Três Passos
4321956,RS,Trindade do Sul
4322004,RS,Triunfo
4322103,RS,Tucunduva
4322152,RS,Tunas
4322186,RS,Tupanci do Sul
4322202,RS,Tupanciretã
4322251,RS,Tupandi
4322301,RS,Tuparendi
4322327,RS,Turuçu
4322343,RS,Ubiretama
4322350,RS,União da Serra
4322376,RS,Unistalda
4322400,RS,Uruguaiana
4322509,RS,Vacaria
4322533,RS,Vale do Sol
4322541,RS,Vale Real
4322525,RS,Vale Verde
4322558,RS,Vanini
4322608,RS,Venâncio Aires
4322707,RS,Vera Cruz
4322806,RS,Veranópolis
4322855,RS,Vespasiano Corrêa
4322905,RS,Viadutos
4323002,RS,Viamão
4323101,RS,Vicente Dutra
4323200,RS,Victor Graeff
4323309,RS,Vila Flores
4323358,RS,Vila Lângaro
4323408,RS,Vila Maria
4323457,RS,Vila Nova do Sul
4323507,RS,Vista Alegre
4323606,RS,Vista Alegre do Prata
4323705,RS,Vista Gaúcha
4323754,RS,Vitória das Missões
4323770,RS,Westfália
4323804,RS,Xangri-lá
5000203,MS,Água Clara
5000252,MS,Alcinópolis
5000609,MS,Amambai
5000708,MS,Anastácio
5000807,MS,Anaurilândia
5000856,MS,Angélica
5000906,MS,Antônio João
5001003,MS,Aparecida do Taboado
5001102,MS,Aquidauana
5001243,MS,Aral Moreira
5001508,MS,Bandeirantes
5001904,MS,Bataguassu
5002001,MS,Batayporã
5002100,MS,Bela Vista
5002159,MS,Bodoquena
5002209,MS,Bonito
5002308,MS,Brasilândia
5002407,MS,Caarapó
5002605,MS,Camapuã
5002704,MS,Campo Grande
5002803,MS,Caracol
5002902,MS,Cassilândia
5002951,MS,Chapadão do Sul
5003108,MS,Corguinho
5003157,MS,Coronel Sapucaia
5003207,MS,Corumbá
5003256,MS,Costa Rica
5003306,MS,Coxim
5003454,MS,Deodápolis
5003488,MS,Dois Irmãos do Buriti
5003504,MS,Douradina
5003702,MS,Dourados
5003751,MS,Eldorado
5003801,MS,Fátima do Sul
5003900,MS,Figueirão
5004007,MS,Glória de Dourados
5004106,MS,Guia Lopes da Laguna
5004304,MS,Iguatemi
5004403,MS,Inocência
5004502,MS,Itaporã
5004601,MS,Itaquiraí
5004700,MS,Ivinhema
5004809,MS,Japorã
5004908,MS,Jaraguari
5005004,MS,Jardim
5005103,MS,Jateí
5005152,MS,Juti
5005202,MS,Ladário
5005251,MS,Laguna Carapã
5005400,MS,Maracaju
5005608,MS,Miranda
5005681,MS,Mundo Novo
5005707,MS,Naviraí
5005806,MS,Nioaque
5006002,MS,Nova Alvorada do Sul
5006200,MS,Nova Andradina
5006259,MS,Novo Horizonte do Sul
5006275,MS,Paraíso das Águas
5006309,MS,Paranaíba
5006358,MS,Paranhos
5006408,MS,Pedro Gomes
5006606,MS,Ponta Porã
5006903,MS,Porto Murtinho
5007109,MS,Ribas do Rio Pardo
5007208,MS,Rio Brilhante
5007307,MS,Rio Negro
5007406,MS,Rio Verde de Mato Grosso
5007505,MS,Rochedo
5007554,MS,Santa Rita do Pardo
5007695,MS,São Gabriel do Oeste
5007802,MS,Selvíria
5007703,MS,Sete Quedas
5007901,MS,Sidrolândia
5007935,MS,Sonora
5007950,MS,Tacuru
5007976,MS,Taquarussu
5008008,MS,Terenos
5008305,MS,Três Lagoas
5008404,MS,Vicentina
5100102,MT,Acorizal
5100201,MT,Água Boa
5100250,MT,Alta Floresta
5100300,MT,Alto Araguaia
5100359,MT,Alto Boa Vista
5100409,MT,Alto Garças
5100508,MT,Alto Paraguai
5100607,MT,Alto Taquari
5100805,MT,Apiacás
5101001,MT,Araguaiana
5101209,MT,Araguainha
5101258,MT,Araputanga
5101308,MT,Arenápolis
5101407,MT,Aripuanã
5101605,MT,Barão de Melgaço
5101704,MT,Barra do Bugres
5101803,MT,Barra do Garças
5101852,MT,Bom Jesus do Araguaia
5101902,MT,Brasnorte
5102504,MT,Cáceres
5102603,MT,Campinápolis
5102637,MT,Campo Novo do Parecis
5102678,MT,Campo Verde
5102686,MT,Campos de Júlio
5102694,MT,Canabrava do Norte
5102702,MT,Canarana
5102793,MT,Carlinda
5102850,MT,Castanheira
5103007,MT,Chapada dos Guimarães
5103056,MT,Cláudia
5103106,MT,Cocalinho
5103205,MT,Colíder
5103254,MT,Colniza
5103304,MT,Comodoro
5103353,MT,Confresa
5103361,MT,Conquista Oeste
5103379,MT,Cotriguaçu
5103403,MT,Cuiabá
5103437,MT,Curvelândia
5103452,MT,Denise
5103502,MT,Diamantino
5103601,MT,Dom Aquino
5103700,MT,Feliz Natal
5103809,MT,Figueirópolis Oeste
5103858,MT,Gaúcha do Norte
5103908,MT,General Carneiro
5103957,MT,Glória Oeste
5104104,MT,Guarantã do Norte
5104203,MT,Guiratinga
5104500,MT,Indiavaí
5104526,MT,Ipiranga do Norte
5104542,MT,Itanhangá
5104559,MT,Itaúba
5104609,MT,Itiquira
5104807,MT,Jaciara
5104906,MT,Jangada
5105002,MT,Jauru
5105101,MT,Juara
5105150,MT,Juína
5105176,MT,Juruena
5105200,MT,Juscimeira
5105234,MT,Lambari Oeste
5105259,MT,Lucas do Rio Verde
5105309,MT,Luciara
5105580,MT,Marcelândia
5105606,MT,Matupá
5105622,MT,Mirassol Oeste
5105903,MT,Nobres
5106000,MT,Nortelândia
5106109,MT,Nossa Senhora do Livramento
5106158,MT,Nova Bandeirantes
5106208,MT,Nova Brasilândia
5106216,MT,Nova Canaã do Norte
5108808,MT,Nova Guarita
5106182,MT,Nova Lacerda
5108857,MT,Nova Marilândia
5108907,MT,Nova Maringá
5108956,MT,Nova Monte Verde
5106224,MT,Nova Mutum
5106174,MT,Nova Nazaré
5106232,MT,Nova Olímpia
5106190,MT,Nova Santa Helena
5106240,MT,Nova Ubiratã
5106257,MT,Nova Xavantina
5106273,MT,Novo Horizonte do Norte
5106265,MT,Novo Mundo
5106315,MT,Novo Santo Antônio
5106281,MT,Novo São Joaquim
5106299,MT,Paranaíta
5106307,MT,Paranatinga
5106372,MT,Pedra Preta
5106422,MT,Peixoto de Azevedo
5106455,MT,Planalto da Serra
5106505,MT,Poconé
5106653,MT,Pontal do Araguaia
5106703,MT,Ponte Branca
5106752,MT,Pontes e Lacerda
5106778,MT,Porto Alegre do Norte
5106802,MT,Porto dos Gaúchos
5106828,MT,Porto Esperidião
5106851,MT,Porto Estrela
5107008,MT,Poxoréu
5107040,MT,Primavera do Leste
5107065,MT,Querência
5107156,MT,Reserva do Cabaçal
5107180,MT,Ribeirão Cascalheira
5107198,MT,Ribeirãozinho
5107206,MT,Rio Branco
5107578,MT,Rondolândia
5107602,MT,Rondonópolis
5107701,MT,Rosário Oeste
5107750,MT,Salto do Céu
5107248,MT,Santa Carmem
5107743,MT,Santa Cruz do Xingu
5107768,MT,Santa Rita do Trivelato
5107776,MT,Santa Terezinha
5107263,MT,Santo Afonso
5107792,MT,Santo Antônio do Leste
5107800,MT,Santo Antônio do Leverger
5107859,MT,São Félix do Araguaia
5107297,MT,São José do Povo
5107305,MT,São José do Rio Claro
5107354,MT,São José do Xingu
5107107,MT,São José dos Quatro Marcos
5107404,MT,São Pedro da Cipa
5107875,MT,Sapezal
5107883,MT,Serra Nova Dourada
5107909,MT,Sinop
5107925,MT,Sorriso
5107941,MT,Tabaporã
5107958,MT,Tangará da Serra
5108006,MT,Tapurah
5108055,MT,Terra Nova do Norte
5108105,MT,Tesouro
5108204,MT,Torixoréu
5108303,MT,União do Sul
5108352,MT,Vale de São Domingos
5108402,MT,Várzea Grande
5108501,MT,Vera
5105507,MT,Vila Bela da Santíssima Trindade
5108600,MT,Vila Rica
5200050,GO,Abadia de Goiás
5200100,GO,Abadiânia
5200134,GO,Acreúna
5200159,GO,Adelândia
5200175,GO,Água Fria de Goiás
5200209,GO,Água Limpa
5200258,GO,Águas Lindas de Goiás
5200308,GO,Alexânia
5200506,GO,Aloândia
5200555,GO,Alto Horizonte
5200605,GO,Alto Paraíso de Goiás
5200803,GO,Alvorada do Norte
5200829,GO,Amaralina
5200852,GO,Americano do Brasil
5200902,GO,Amorinópolis
5201108,GO,Anápolis
5201207,GO,Anhanguera
5201306,GO,Anicuns
5201405,GO,Aparecida de Goiânia
5201454,GO,Aparecida do Rio Doce
5201504,GO,Aporé
5201603,GO,Araçu
5201702,GO,Aragarças
5201801,GO,Aragoiânia
5202155,GO,Araguapaz
5202353,GO,Arenópolis
5202502,GO,Aruanã
5202601,GO,Aurilândia
5202809,GO,Avelinópolis
5203104,GO,Baliza
5203203,GO,Barro Alto
5203302,GO,Bela Vista de Goiás
5203401,GO,Bom Jardim de Goiás
5203500,GO,Bom Jesus de Goiás
5203559,GO,Bonfinópolis
5203575,GO,Bonópolis
5203609,GO,Brazabrantes
5203807,GO,Britânia
5203906,GO,Buriti Alegre
5203939,GO,Buriti de Goiás
5203962,GO,Buritinópolis
5204003,GO,Cabeceiras
5204102,GO,Cachoeira Alta
5204201,GO,Cachoeira de Goiás
5204250,GO,Cachoeira Dourada
5204300,GO,Caçu
5204409,GO,Caiapônia
5204508,GO,Caldas Novas
5204557,GO,Caldazinha
5204607,GO,Campestre de Goiás
5204656,GO,Campinaçu
5204706,GO,Campinorte
5204805,GO,Campo Alegre de Goiás
5204854,GO,Campo Limpo de Goiás
5204904,GO,Campos Belos
5204953,GO,Campos Verdes
5205000,GO,Carmo do Rio Verde
5205059,GO,Castelândia
5205109,GO,Catalão
5205208,GO,Caturaí
5205307,GO,Cavalcante
5205406,GO,Ceres
5205455,GO,Cezarina
5205471,GO,Chapadão do Céu
5205497,GO,Cidade Ocidental
5205513,GO,Cocalzinho de Goiás
5205521,GO,Colinas do Sul
5205703,GO,Córrego do Ouro
5205802,GO,Corumbá de Goiás
5205901,GO,Corumbaíba
5206206,GO,Cristalina
5206305,GO,Cristianópolis
5206404,GO,Crixás
5206503,GO,Cromínia
5206602,GO,Cumari
5206701,GO,Damianópolis
5206800,GO,Damolândia
5206909,GO,Davinópolis
5207105,GO,Diorama
5208301,GO,Divinópolis de Goiás
5207253,GO,Doverlândia
5207352,GO,Edealina
5207402,GO,Edéia
5207501,GO,Estrela do Norte
5207535,GO,Faina
5207600,GO,Fazenda Nova
5207808,GO,Firminópolis
5207907,GO,Flores de Goiás
5208004,GO,Formosa
5208103,GO,Formoso
5208152,GO,Gameleira de Goiás
5208400,GO,Goianápolis
5208509,GO,Goiandira
5208608,GO,Goianésia
5208707,GO,Goiânia
5208806,GO,Goianira
5208905,GO,Goiás
5209101,GO,Goiatuba
5209150,GO,Gouvelândia
5209200,GO,Guapó
5209291,GO,Guaraíta
5209408,GO,Guarani de Goiás
5209457,GO,Guarinos
5209606,GO,Heitoraí
5209705,GO,Hidrolândia
5209804,GO,Hidrolina
5209903,GO,Iaciara
5209937,GO,Inaciolândia
5209952,GO,Indiara
5210000,GO,Inhumas
5210109,GO,Ipameri
5210158,GO,Ipiranga de Goiás
5210208,GO,Iporá
5210307,GO,Israelândia
5210406,GO,Itaberaí
5210562,GO,Itaguari
5210604,GO,Itaguaru
5210802,GO,Itajá
5210901,GO,Itapaci
5211008,GO,Itapirapuã
5211206,GO,Itapuranga
5211305,GO,Itarumã
5211404,GO,Itauçu
5211503,GO,Itumbiara
5211602,GO,Ivolândia
5211701,GO,Jandaia
5211800,GO,Jaraguá
5211909,GO,Jataí
5212006,GO,Jaupaci
5212055,GO,Jesúpolis
5212105,GO,Joviânia
5212204,GO,Jussara
5212253,GO,Lagoa Santa
5212303,GO,Leopoldo de Bulhões
5212501,GO,Luziânia
5212600,GO,Mairipotaba
5212709,GO,Mambaí
5212808,GO,Mara Rosa
5212907,GO,Marzagão
5212956,GO,Matrinchã
5213004,GO,Maurilândia
5213053,GO,Mimoso de Goiás
5213087,GO,Minaçu
5213103,GO,Mineiros
5213400,GO,Moiporá
5213509,GO,Monte Alegre de Goiás
5213707,GO,Montes Claros de Goiás
5213756,GO,Montividiu
5213772,GO,Montividiu do Norte
5213806,GO,Morrinhos
5213855,GO,Morro Agudo de Goiás
5213905,GO,Mossâmedes
5214002,GO,Mozarlândia
5214051,GO,Mundo Novo
5214101,GO,Mutunópolis
5214408,GO,Nazário
5214507,GO,Nerópolis
5214606,GO,Niquelândia
5214705,GO,Nova América
5214804,GO,Nova Aurora
5214838,GO,Nova Crixás
5214861,GO,Nova Glória
5214879,GO,Nova Iguaçu de Goiás
5214903,GO,Nova Roma
5215009,GO,Nova Veneza
5215207,GO,Novo Brasil
5215231,GO,Novo Gama
5215256,GO,Novo Planalto
5215306,GO,Orizona
5215405,GO,Ouro Verde de Goiás
5215504,GO,Ouvidor
5215603,GO,Padre Bernardo
5215652,GO,Palestina de Goiás
5215702,GO,Palmeiras de Goiás
5215801,GO,Palmelo
5215900,GO,Palminópolis
5216007,GO,Panamá
5216304,GO,Paranaiguara
5216403,GO,Paraúna
5216452,GO,Perolândia
5216809,GO,Petrolina de Goiás
5216908,GO,Pilar de Goiás
5217104,GO,Piracanjuba
5217203,GO,Piranhas
5217302,GO,Pirenópolis
5217401,GO,Pires do Rio
5217609,GO,Planaltina
5217708,GO,Pontalina
5218003,GO,Porangatu
5218052,GO,Porteirão
5218102,GO,Portelândia
5218300,GO,Posse
5218391,GO,Professor Jamil
5218508,GO,Quirinópolis
5218607,GO,Rialma
5218706,GO,Rianápolis
5218789,GO,Rio Quente
5218805,GO,Rio Verde
5218904,GO,Rubiataba
5219001,GO,Sanclerlândia
5219100,GO,Santa Bárbara de Goiás
5219209,GO,Santa Cruz de Goiás
5219258,GO,Santa Fé de Goiás
5219308,GO,Santa Helena de Goiás
5219357,GO,Santa Isabel
5219407,GO,Santa Rita do Araguaia
5219456,GO,Santa Rita do Novo Destino
5219506,GO,Santa Rosa de Goiás
5219605,GO,Santa Tereza de Goiás
5219704,GO,Santa Terezinha de Goiás
5219712,GO,Santo Antônio da Barra
5219738,GO,Santo Antônio de Goiás
5219753,GO,Santo Antônio do Descoberto
5219803,GO,São Domingos
5219902,GO,São Francisco de Goiás
5220058,GO,São João da Paraúna
5220009,GO,São João Aliança
5220108,GO,São Luís de Montes Belos
5220157,GO,São Luiz do Norte
5220207,GO,São Miguel do Araguaia
5220264,GO,São Miguel do Passa Quatro
5220280,GO,São Patrício
5220405,GO,São Simão
5220454,GO,Senador Canedo
5220504,GO,Serranópolis
5220603,GO,Silvânia
5220686,GO,Simolândia
5220702,GO,Sítio Abadia
5221007,GO,Taquaral de Goiás
5221080,GO,Teresina de Goiás
5221197,GO,Terezópolis de Goiás
5221304,GO,Três Ranchos
5221403,GO,Trindade
5221452,GO,Trombas
5221502,GO,Turvânia
5221551,GO,Turvelândia
5221577,GO,Uirapuru
5221601,GO,Uruaçu
5221700,GO,Uruana
5221809,GO,Urutaí
5221858,GO,Valparaíso de Goiás
5221908,GO,Varjão
5222005,GO,Vianópolis
5222054,GO,Vicentinópolis
5222203,GO,Vila Boa
5222302,GO,Vila Propício
5300108,DF,Brasília";
        foreach (var row in cities.Split('\n'))
        {
            var columns = row.Split(',');
            var code = columns[0];
            var state = columns[1];
            var city = Regex.Replace(columns[2], "\\r", string.Empty);
            
            yield return new City {
                Id = id++, 
                Code = code, 
                State = state,
                Name = city
            };
        }
    }
}