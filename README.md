# Sistema de Gestão de Pessoas

Aplicação web desenvolvida em ASP.NET Core MVC com Entity Framework Core e SQL Server, para auxiliar gestores de RH no acompanhamento de setores, funcionários, metas e desempenhos.

---

## 📋 Funcionalidades Principais

 **Cadastro de Setores** 
- Define o nome e capacidade máxima de pessoas  
- Impede o cadastro de novos funcionários acima da capacidade

 **Cadastro de Funcionários**  
- Campos: nome, cargo, salário, data de admissão e setor  
- Validação para evitar ultrapassar o limite de capacidade do setor  

 **Cadastro de Metas**  
- Cada meta pertence a um setor  
- Define descrição e pontuação máxima  

 **Registro de Desempenho**  
- Permite registrar a pontuação de um funcionário em uma meta  
- Cada funcionário pode ter várias metas avaliadas  

 **Relatórios e Indicadores**
- Quantidade de funcionários por setor  
- Média de desempenho por setor  
- Melhor funcionário da empresa  
- Setor com melhor desempenho  
- Gráficos dinâmicos com **Chart.js**

 **Dashboard Resumido**
- Exibe total de funcionários  
- Melhor funcionário e melhor setor  
- Ranking com pontos totais  
- Gráfico de desempenho total por setor  

---

## 🧰 Tecnologias Utilizadas

| Camada | Tecnologia |
|:-------|:------------|
| **Backend** | ASP.NET Core MVC |
| **Banco de Dados** | SQL Server (LocalDB) |
| **ORM** | Entity Framework Core (Code First) |
| **Frontend** | HTML5, CSS3, Bootstrap 5, JavaScript |
| **Gráficos** | Chart.js |
| **Consultas** | LINQ |

---

## ⚙️ Como Executar o Projeto Localmente

### 1️⃣ Pré-requisitos

- [.NET SDK 8.0](https://dotnet.microsoft.com/en-us/download) (ou superior)  
- SQL Server ou LocalDB instalado  
- Visual Studio / Visual Studio Code  

---

### 2️⃣ Clonar o Repositório

git clone https://github.com/GabrielResendel/GestaoDePessoas.git
cd GestaoDePessoas
3️⃣ Configurar a String de Conexão
Abra o arquivo appsettings.json e ajuste conforme seu ambiente:

json
Copiar código
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=GestaoPessoasDB;Trusted_Connection=True;MultipleActiveResultSets=true"
}
Se estiver usando outro SQL Server, altere o Server e o Database conforme necessário.

4️⃣ Criar o Banco de Dados
Execute o comando:

bash
Copiar código
dotnet ef database update
Isso criará automaticamente as tabelas com base nos modelos (Code First).

5️⃣ Rodar o Projeto
bash
Copiar código
dotnet run
Ou, pelo Visual Studio, clique em ▶️ “Executar”.

Acesse no navegador:
👉 http://localhost:5000 ou https://localhost:5001

🧭 Estrutura do Projeto
vbnet
Copiar código
GestaoDePessoas/
├── Controllers/
│   ├── DashboardController.cs
│   ├── DesempenhosController.cs
│   ├── FuncionariosController.cs
│   ├── MetasController.cs
│   ├── RelatoriosController.cs
│   ├── SetoresController.cs
│   └── HomeController.cs
│
├── Models/
│   ├── Setor.cs
│   ├── Funcionario.cs
│   ├── Meta.cs
│   └── Desempenho.cs
│
├── Data/
│   └── GestaoPessoasContext.cs
│
├── Views/
│   ├── Dashboard/
│   ├── Desempenhos/
│   ├── Funcionarios/
│   ├── Metas/
│   ├── Relatorios/
│   ├── Setores/
│   └── Shared/
│
├── wwwroot/
│   ├── css/
│   ├── js/
│   └── lib/
│
├── appsettings.json
├── Program.cs
└── README.md

 **Como Utilizar o Sistema**
Acesse o Dashboard: veja total de funcionários, setor destaque e melhor funcionário.

Cadastre Setores: defina nome e capacidade máxima.

Cadastre Funcionários: associe-os a setores respeitando a capacidade.

Crie Metas: defina metas para cada setor.

Registre Desempenhos: atribua pontuações de metas a funcionários.

Acesse Relatórios: veja médias de desempenho, número de funcionários e gráficos comparativos.

Exclua registros com segurança: há modais de confirmação antes da exclusão.

 **Recursos Extras**
Gráficos responsivos com Chart.js

Validações dinâmicas no cadastro

Interface moderna e intuitiva com Bootstrap 5

Mensagens de sucesso e erro no fluxo CRUD

Dashboard interativo consolidando resultados

🧑‍💻 Autor
Gabriel Resende
📧 codegabriel7@gmail.com
