# CRUD C# .NET com MySQL

## Antes de iniciar certifique-se de ter o c# .NET instalado em sua máquina, esse programa foi desenvolvido pelo visual studio que pode ser baixado <a href="https://visualstudio.microsoft.com/pt-br/thank-you-downloading-visual-studio/?sku=Community&channel=Release&version=VS2022&source=VSLandingPage&cid=2030&passive=false">clicando aqui</a>.

## Ao clonar este proejeto abra um terminal na pasta raiz e insira o seguinte comando

```bash
dotnet restore
```

# Isso instalarará as dependências necessárias para rodar a aplicação.

**Lembre-se que você terá que modificar o appsetings.json na raiz do projeto e colocar a senha do banco de dados juntamente as credenciais mencionadas**

## Depois de fazer isso acesse o conteúdo de crudcs_pessoa.sql na pasta dabase na raiz do proejo e copie o código deste arquivo

1. Abra o MySQL Workbanch
2. Insira o código copiado dentro de um bloco SQL
3. Veja ao lado esquerdo o banco

## Depois de realizar essas etapas você terá a aplicação configurada, basta acessar o app e rodá-lo, caso esteja em outra IDE insira o comando na linha do terminal correspondente a raiz do projeto:

```bash
dotnet run
```

# Por padrão você pode acessar http://localhost:5000/criar.html no seu chrome e começar a manipulação dos dados:

## Roadmap

1. **Para criar pessoa:** Acesse http://localhost:5000/criar.html
   <br>
   \*você pode adicionar mais pessoas de uma só vez acrescentado-as na lista clicando no botão **Acrescentar pessoa na lista** e depois em **Enviar\***

2. **Para listar pessoa(a) cadastrada(s):** http://localhost:5000/listar.html
   <br>
   \*Existe um campo CPF que será o parâmetro de pesquisa, porém caso você não inisira nada ele retornará todas as pessoas ao clicar em **Consultar\***

3. **Para atualizar pessoa:** Acesse http://localhost:5000/atualizar.html
   <br>
   \*Para atualizar uma pessoa basta inserir o CPF de quem será atualizado, inserir os dados que serão sobrescritos, e clicar em **Atualizar\***

4. **Para deletar pessoa:** Acesse http://localhost:5000/deletar.html
   <br>
   \*Para atualizar uma pessoa basta inserir o CPF de quem será deletado e em seguida clicar em **Deletar\***

## Caso não queira a ferramenta visual use Insomnia

**Rotas do servidor:**

1. **Cria pessoa(a):** http://localhost:5000/api/Pessoa/create

   ```json
   // Corpo JSON

   [
     {
       "Nome": "Ana",
       "Idade": 30,
       "CPF": "12345678900",
       "Email": "ana@exampele.com"
     },
     {
       "Nome": "João",
       "Idade": 25,
       "CPF": "98765432100",
       "Email": "joao@example.com"
     },
     {
       "Nome": "Maria",
       "Idade": 35,
       "CPF": "55544433300",
       "Email": "maria@example.com"
     },
     {
       "Nome": "Pedro",
       "Idade": 40,
       "CPF": "11122233300",
       "Email": "pedro@example.com"
     }
   ]
   ```

2. **Listar todas as pessoas:** http://localhost:5000/api/Pessoa/listar

3. **Listar pessoa com filtro:** http://localhost:5000/api/Pessoa/listar?cpf=CPF_AQUI

4. **Atualizar pessoa com filtro:** http://localhost:5000/api/Pessoa/atualizar?cpf=CPF_AQUI

   ```json
   //Corpo JSON

   {
     "Nome": "Novo nome",
     "Idade": 123,
     "cpf": "null",
     "Email": "novoemail.com"
   }
   ```

5. **Deletar pessoa com filtro:** http://localhost:5000/api/Pessoa/deletar?cpf=CPF_AQUI
