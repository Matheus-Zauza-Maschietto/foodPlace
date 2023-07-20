<h1>Food Place</h1>
<p>
    Restful API de um sistema de lojas
</p>
<hr>
<h2>Introdução</h2>
<p>
    Esse projeto pretende simular um sistema de integração que
    possibilite que vendedores publiquem suas lojas online, permitindo
    um máximo de 3 lojas por pessoa. Cada loja isolada
    das demais, de modo que apenas o próprio criador tenha seu acesso
    aos seus recursos. <br>
    Permite criação de uma loja, alteração de seus dados, visualização e deleção dos mesmos<br>
    Para seus Produtos, é permitido sua criação, alteração completa dos dados, alteração da disponibilidade, deleção do
    registro, visualização de um único
    produto, ou de todos os produtos pertencentes a uma de suas lojas
</p>
<h3>Recomendações</h3>
<p>
    Como existem validações para verificação de CPNJs e CPFs validos, aqui estão 2 links para gerar CNPJs e CPFs
    validos: <br>
    <code>https://www.4devs.com.br/gerador_de_cnpj</code> - CNPJ <br>
    <code>https://www.4devs.com.br/gerador_de_cpf</code> - CPF <br>
</p>
<hr>
<h2>Instalação</h2>
<h3>Projeto Dotnet</h3>
<p>
    Dotnet 6.0 <br>
    Flunt 2.0.5 <br>
    Microsoft.AspNetCore.Authentication.JwtBearer 6 <br>
    Microsoft.AspNetCore.Identity.EntityFrameworkCore 6 <br>
    Microsoft.EntityFrameworkCore 7.0.9 <br>
    Microsoft.EntityFrameworkCore.Design 7.0.9 <br>
    Microsoft.EntityFrameworkCore.SqlServer 7.0.9 <br>
    Swashbuckle.AspNetCore 6.5.0 <br>
</p>
<h3>Docker Desktop</h3>
<p>
    Utilizar WSL2 <br>
    Imagem de banco usada: <code>docker pull mcr.microsoft.com/mssql/server:2019-latest</code> <br>
    Instalação do container:
    <code>docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=@Sql2019" -p 1433:1433 --name FoodPlace --hostname FoodPlace -d   mcr.microsoft.com/mssql/server:2019-latest</code>
</p>
<hr/>
<h2>Configuração</h2>
<p>
    A API funciona através do uso de autenticação via Json Web Token (JWT), portanto sendo necessario primeiro cadastrar
    um usuário e apenas após isso é possível
    acessar os demais endpoints
</p>
<hr/>
<h2>Documentação dos endpoints</h2>
<p>
    Para documentação da API, fica à disposição a ferramenta SWAGGER. <br>
    Pode ser acessada após inicialização do projeto na URL:
    <code>https://localhost:[porta-usada]/swagger/index.html</code>
</p>
<hr/>
<h2>Implementações Futuras</h2>
<p>
    Para implementações futuras existe o cadastro de um endereço para as lojas, fazendo uso de um middleware entre as
    API do IBGE: <br>
    
      <code>https://servicodados.ibge.gov.br/api/v1/localidades/estados<br></code><br>
      <code>https://servicodados.ibge.gov.br/api/v1/localidades/municipios<br></code>
    
    Categorias são importantes para filtragem e distinção, sendo assim outra próxima implementação. Permitir que o
    vendedor possa cadastrar suas próprias
    categorias, editá-las e excluí-las, além de atribuir a seus produtos elas.
</p>
