O Projeto foi constrido em .net(core 3.1) com SqlServer,
Para executar abra no visual studio 2019 e copile.

Antes de executar o projeto, execute os escripts iniciais do banco de dados que estão dentro da pasta "Script".
Configura o SpotifyClienteID e o SpotifyClienteSecret dentro do appsettings
Configure a string de conexão no arquivo appsettings na tag ConnectionStrings

o primeiro método a ser executado está dentro da api do spotify
"api/spotify/cargainicial"
Este método irá buscar os albuns por genero através da api do spotify
