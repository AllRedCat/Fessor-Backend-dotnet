# FessorApi

## 📋 Descrição

FessorApi é uma API REST desenvolvida em ASP.NET Core 8.0 para gerenciamento de escolas, estudantes, usuários e relatórios. A aplicação utiliza autenticação baseada em cookies e Entity Framework Core com MySQL como banco de dados.

## 🏗️ Arquitetura

### Tecnologias Utilizadas

- **.NET 8.0** - Framework principal
- **ASP.NET Core Web API** - Para criação da API REST
- **Entity Framework Core** - ORM para acesso ao banco de dados
- **MySQL** - Banco de dados relacional
- **Pomelo.EntityFrameworkCore.MySql** - Provedor MySQL para EF Core
- **Swagger/OpenAPI** - Documentação da API
- **Cookie Authentication** - Sistema de autenticação

### Estrutura do Projeto

```
FessorApi/
├── Controllers/          # Controladores da API
├── Data/                # Contexto do banco de dados
├── Models/              # Modelos de dados
├── Migrations/          # Migrações do banco de dados
├── Properties/          # Configurações do projeto
├── Program.cs           # Ponto de entrada da aplicação
├── appsettings.json     # Configurações da aplicação
└── FessorApi.csproj     # Arquivo do projeto
```

## 🗄️ Modelos de Dados

### User (Usuário)
```csharp
public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Document { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public string ProfilePicture { get; set; }
    public int? SchoolId { get; set; }
    public School? School { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public List<Report> Reports { get; set; }
}
```

### School (Escola)
```csharp
public class School
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
    public string Principal { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public List<Student> Students { get; set; }
    public List<User> Users { get; set; }
    public List<Report> Reports { get; set; }
}
```

### Student (Estudante)
```csharp
public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Document { get; set; }
    public string Email { get; set; }
    public int SchoolId { get; set; }
    public School School { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public List<Report> Reports { get; set; }
}
```

### Report (Relatório)
```csharp
public class Report
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public Student Student { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public int SchoolId { get; set; }
    public School School { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
```

### Demo (Demonstração)
```csharp
public class Demo
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
```

## 🔗 Relacionamentos

- **School → Students**: Uma escola pode ter múltiplos estudantes (1:N)
- **School → Users**: Uma escola pode ter múltiplos usuários (1:N) - opcional
- **School → Reports**: Uma escola pode ter múltiplos relatórios (1:N)
- **Student → Reports**: Um estudante pode ter múltiplos relatórios (1:N)
- **User → Reports**: Um usuário pode criar múltiplos relatórios (1:N)

## 🔐 Autenticação e Autorização

### Sistema de Autenticação
- Utiliza **Cookie Authentication**
- Endpoints de login/logout em `/auth`
- Autenticação baseada em email e senha
- Claims personalizadas para ID do usuário e role

### Endpoints de Autenticação

#### POST /auth/login
```json
{
    "email": "usuario@exemplo.com",
    "password": "senha123"
}
```

#### POST /auth/logout
- Remove o cookie de autenticação

## 📡 Endpoints da API

### Usuários (`/api/users`)
- `GET /api/users` - Lista todos os usuários
- `GET /api/users/{id}` - Obtém usuário por ID
- `POST /api/users` - Cria novo usuário (sem autenticação)
- `PUT /api/users/{id}` - Atualiza usuário
- `DELETE /api/users/{id}` - Remove usuário

### Escolas (`/api/schools`)
- `GET /api/schools` - Lista todas as escolas
- `GET /api/schools/{id}` - Obtém escola por ID
- `POST /api/schools` - Cria nova escola
- `PUT /api/schools/{id}` - Atualiza escola
- `DELETE /api/schools/{id}` - Remove escola

### Estudantes (`/api/students`)
- `GET /api/students` - Lista todos os estudantes
- `GET /api/students/{id}` - Obtém estudante por ID
- `POST /api/students` - Cria novo estudante
- `PUT /api/students/{id}` - Atualiza estudante
- `DELETE /api/students/{id}` - Remove estudante

### Relatórios (`/api/reports`)
- `GET /api/reports` - Lista todos os relatórios
- `GET /api/reports/{id}` - Obtém relatório por ID
- `POST /api/reports` - Cria novo relatório
- `PUT /api/reports/{id}` - Atualiza relatório
- `DELETE /api/reports/{id}` - Remove relatório

### Demonstrações (`/api/demos`)
- `GET /api/demos` - Lista todas as demonstrações
- `GET /api/demos/{id}` - Obtém demonstração por ID
- `POST /api/demos` - Cria nova demonstração
- `PUT /api/demos/{id}` - Atualiza demonstração
- `DELETE /api/demos/{id}` - Remove demonstração

## ⚙️ Configuração

### Configurações do Banco de Dados
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "server=192.168.15.10;port=3306;database=dotnet;user=admin;password=password"
  }
}
```

### Configurações do Servidor
```json
{
  "Kestrel": {
    "Endpoints": {
      "Http": {
        "Url": "http://0.0.0.0:5005"
      },
      "Https": {
        "Url": "https://0.0.0.0:5006"
      }
    }
  }
}
```

## 🚀 Como Executar

### Pré-requisitos
- .NET 8.0 SDK
- MySQL Server
- Visual Studio 2022 ou VS Code

### Passos para Execução

1. **Clone o repositório**
   ```bash
   git clone <url-do-repositorio>
   cd FessorApi
   ```

2. **Configure o banco de dados**
   - Certifique-se de que o MySQL está rodando
   - Atualize a string de conexão em `appsettings.json`

3. **Execute as migrações**
   ```bash
   dotnet ef database update
   ```

4. **Execute a aplicação**
   ```bash
   dotnet run
   ```

5. **Acesse a documentação**
   - Swagger UI: `https://localhost:5006/swagger`
   - API: `https://localhost:5006`

## 📦 Dependências

### Pacotes NuGet Principais
- `Microsoft.AspNetCore.Authentication.Cookies` (2.3.0)
- `Microsoft.AspNetCore.OpenApi` (8.0.16)
- `Microsoft.EntityFrameworkCore.Design` (8.0.13)
- `Pomelo.EntityFrameworkCore.MySql` (8.0.3)
- `Swashbuckle.AspNetCore` (6.6.2)

## 🔧 Desenvolvimento

### Estrutura de Controllers
Todos os controllers seguem o padrão REST com operações CRUD:
- `GET` - Listar/Obter recursos
- `POST` - Criar novos recursos
- `PUT` - Atualizar recursos existentes
- `DELETE` - Remover recursos

### Middleware Pipeline
1. HTTPS Redirection
2. Authentication
3. Authorization
4. Controllers

### Migrações
O projeto inclui migrações para:
- Criação inicial das tabelas
- Tornar SchoolId opcional para usuários

## 📝 Notas de Segurança

⚠️ **Atenção**: Esta é uma implementação básica. Para produção, considere:

- Implementar hash de senhas (bcrypt, Argon2)
- Adicionar validação de entrada
- Implementar rate limiting
- Configurar HTTPS adequadamente
- Adicionar logging de auditoria
- Implementar refresh tokens
- Adicionar validação de dados com FluentValidation

## 🤝 Contribuição

1. Faça um fork do projeto
2. Crie uma branch para sua feature (`git checkout -b feature/AmazingFeature`)
3. Commit suas mudanças (`git commit -m 'Add some AmazingFeature'`)
4. Push para a branch (`git push origin feature/AmazingFeature`)
5. Abra um Pull Request

## 📄 Licença

Este projeto está sob a licença MIT. Veja o arquivo `LICENSE` para mais detalhes.

## 📞 Suporte

Para suporte, envie um email para [seu-email@exemplo.com] ou abra uma issue no repositório.
