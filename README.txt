O Projeto foi constrido em .net(core 3.1) com SqlServer,
Para executar abra no visual studio 2019 e copile.

Antes de executar o projeto, execute os escripts iniciais do banco de dados que est�o dentro da pasta "Script".
Configura o SpotifyClienteID e o SpotifyClienteSecret dentro do appsettings
Configure a string de conex�o no arquivo appsettings na tag ConnectionStrings

o primeiro m�todo a ser executado est� dentro da api do spotify
"api/spotify/cargainicial"
Este m�todo ir� buscar os albuns por genero atrav�s da api do spotify
