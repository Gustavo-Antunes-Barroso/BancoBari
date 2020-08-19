Primeiramente crie a file no RabbitMQ apartir de uma imagem no Docker utilizando o seguinte comando:<br />
docker run -it --rm --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management<br />
Para verificar se está tudo ok, acesse http://localhost:15672/ user: guest senha: guest<br /><br />

Crie seu banco de dados e troque a ConnectionString nos arquivos appsettings.json em ambos projetos.<br />
Rode o seguinte script em seu banco de dados:<br /><br />

CREATE TABLE Sistema<br />
(<br />
	Id UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,<br />
	Nome VARCHAR(MAX) NOT NULL<br />
)<br />
GO<br />
insert into Sistema values('07ccd9ab-c9ee-437a-a992-291417f1f23e','BancoBari.Publisher')<br />
insert into Sistema values('64194c4c-1a8d-4c5c-8bf5-e568af6b320d','BancoBari.Publisher 2')<br />
GO<br />
CREATE TABLE MENSAGEM<br />
(<br />
	Id UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,<br />
	Descricao VARCHAR(MAX) NOT NULL,<br />
	SistemaId UNIQUEIDENTIFIER NOT NULL,<br />
	Integrado bit default 0,<br />
	FOREIGN KEY (SistemaId) REFERENCES Sistema(Id)<br />
)<br />
insert into MENSAGEM values('e7c3f279-6b71-4297-a9e2-6e58c1366f02', 'Hello World', '64194c4c-1a8d-4c5c-8bf5-e568af6b320d',0)<br />
GO<br />

CREATE TABLE Queued<br />
(<br />
	TransactionId UNIQUEIDENTIFIER NOT NULL primary key,<br />
	SistemaId UNIQUEIDENTIFIER NOT NULL,<br />
	MensagemId UNIQUEIDENTIFIER NOT NULL,<br />
	MensagemDescricao VARCHAR(MAX) NOT NULL,<br />
	Data DATETIME NOT NULL,<br />
	FOREIGN KEY (SistemaId) REFERENCES Sistema(Id)<br />
) <br />
<br /><br />
Rode as duas aplicações para validar o funcionamento.<br />
As api's do projeto BancoBari estão documentadas via swagger.<br />
Através da url do RabbitMQ http://localhost:15672/ ou fazendo um simples select na tabela Queued, vemos o funcionamento do publish e do subscriber.
