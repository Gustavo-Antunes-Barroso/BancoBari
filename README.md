Primeiramente crie a file no RabbitMQ apartir de uma imagem no Docker utilizando o seguinte comando:
docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management
Para verificar se está tudo ok, acesse http://localhost:15672/ user: guest senha: guest

Crie seu banco de dados e troque a ConnectionString nos arquivos appsettings.json em ambos projetos.
Rode o seguinte script em seu banco de dados:

CREATE TABLE Sistema
(
	Id UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
	Nome VARCHAR(MAX) NOT NULL
)
GO
insert into Sistema values('07ccd9ab-c9ee-437a-a992-291417f1f23e','BancoBari.Publisher')
insert into Sistema values('64194c4c-1a8d-4c5c-8bf5-e568af6b320d','BancoBari.Publisher 2')
GO
CREATE TABLE MENSAGEM
(
	Id UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
	Descricao VARCHAR(MAX) NOT NULL,
	SistemaId UNIQUEIDENTIFIER NOT NULL,
	Integrado bit default 0,
	FOREIGN KEY (SistemaId) REFERENCES Sistema(Id)
)
insert into MENSAGEM values('e7c3f279-6b71-4297-a9e2-6e58c1366f02', 'Hello World', '64194c4c-1a8d-4c5c-8bf5-e568af6b320d',0)
GO

CREATE TABLE Queued
(
	TransactionId UNIQUEIDENTIFIER NOT NULL primary key,
	SistemaId UNIQUEIDENTIFIER NOT NULL,
	MensagemId UNIQUEIDENTIFIER NOT NULL,
	MensagemDescricao VARCHAR(MAX) NOT NULL,
	Data DATETIME NOT NULL,
	FOREIGN KEY (SistemaId) REFERENCES Sistema(Id)
) 

Rode as duas aplicações para validar o funcionamento.
As api's do projeto BancoBari estão documentadas via swagger.
Através da url do RabbitMQ http://localhost:15672/ ou fazendo um simples select na tabela Queued, vemos o funcionamento do publish e do subscriber.
