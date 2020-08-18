Primeiramente crie a file no RabbitMQ apartir de uma imagem no Docker utilizando o seguinte comando:
docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management
Para verificar se está tudo ok, acesse http://localhost:15672/ user: guest senha: guest

Crie seu banco de dados e troque a ConnectionString nos arquivos appsettings.json em ambos projetos.
Rode o seguinte script em seu banco de dados:

CREATE TABLE MENSAGEM
(
	Id UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
	Descricao VARCHAR(MAX) NOT NULL
)
GO
INSERT INTO MENSAGEM VALUES('3FA85F64-5717-4562-B3FC-2C963F66AFA6', 'Hello World');
GO
CREATE TABLE Queued
(
	SistemaId UNIQUEIDENTIFIER NOT NULL,
	NomeSitema VARCHAR(100) NOT NULL,
	MensagemId UNIQUEIDENTIFIER primary key NOT NULL,
	MensagemDescricao VARCHAR(MAX) NOT NULL,
	TransactionId UNIQUEIDENTIFIER NOT NULL,
	Data DATETIME NOT NULL
)

Rode as duas aplicações para validar o funcionamento.
As api's do projeto BancoBari estão documentadas via swagger.
Através da url do RabbitMQ http://localhost:15672/ ou fazendo um simples select na tabela Queued, vemos o funcionamento do publish e do subscriber.

OBS: Na classe Queue.cs do projeto BancoBari foi trocado o set da propriedade MensagemId para Guid.NewGuid(), a fim de vermos o banco sendo populado.
