# Tech News Oficial Backend

### Descrição
O Tech News Oficial Backend é uma API para gerenciar newsletters, permitindo a criação, leitura, atualização e exclusão de newsletters. A API é construída com ASP.NET Core e utiliza o Supabase como backend de banco de dados.

### Requisitos
- .NET 6.0 ou superior
- Supabase (com uma conta configurada)
- Variáveis de ambiente SUPABASE_URL e SUPABASE_KEY

### Configuração
1. Clone o repositório git clone https://github.com/seuusuario/TechNewsOficialBackend.git
```
cd TechNewsOficialBackend
```
2. Defina as variáveis de ambiente No seu terminal ou em um arquivo de configuração, defina as variáveis: 
```
export SUPABASE_URL="sua_url_do_supabase"
export SUPABASE_KEY="sua_chave_do_supabase"
```
3. Instale as dependências Utilize o seguinte comando para restaurar as dependências do projeto: dotnet restore

### Execução

Para executar a API localmente, use o comando:

```
dotnet run
```
A API estará disponível em `http://localhost:5000`.

### Endpoints

Aqui estão os principais endpoints da API:

1. GET /ping
Responde com um objeto simples para verificar se o servidor está funcionando.

2. POST /newsletter
Cria uma nova newsletter.
```
Body: { "name": "Título da Newsletter", "description": "Descrição da Newsletter" }
```
3. GET /newsletter/{id}
Obtém uma newsletter pelo ID.

4. PUT /newsletter/{id}
Atualiza uma newsletter existente.
```
Body: { "name": "Novo Título", "description": "Nova Descrição" }
```
5. GET /newsletter
Obtém todas as newsletters.

6. DELETE /newsletter/{id}
Deleta uma newsletter pelo ID.

### Contribuições
Sinta-se à vontade para abrir issues ou pull requests para melhorar este projeto.

### Licença
Este projeto está licenciado sob a Apache License 2.0.
