# CDB - Calculadora de Investimento

Projeto de avaliação para desenvolvedor (Diretoria de Desenv. Sistemas de Middle e Back Office).


## Estrutura

- **CDB.Api** - Web API em .NET Framework 4.7.2 para cálculo de CDB
- **CDB.Api.Tests** - Testes unitários (xUnit)
- **CDB.Web** - Aplicação Angular 21 (CLI)

## Pré-requisitos

- **API**: .NET Framework 4.7.2 (ou superior), Visual Studio 2022 ou VS Code
- **Testes**: projeto `CDB.Api.Tests` alinhado em .NET Framework 4.7.2
- **Angular**: Node.js 18+ e npm

## Como executar

### 1. API

```bash
cd CDB.Api
dotnet run
```

A API estará disponível em `http://localhost:5054`

### 2. Angular

```bash
cd CDB.Web
npm install
npm start
```

Acesse `http://localhost:4200`

## Testes

```bash
# Executar testes
dotnet test
```

### Relatório de cobertura em HTML

A cobertura da **camada lógica** (Services + Models) deve ficar acima de 90%. O relatório HTML usa **Coverlet** (já no projeto de testes) para coletar os dados e **ReportGenerator** para gerar as páginas.

**Pré-requisito (uma vez):** instalar o ReportGenerator como ferramenta global:

```bash
dotnet tool install -g dotnet-reportgenerator-globaltool
```

**Gerar o relatório:**

Na raiz do repositório, execute:

```bash
gerar-cobertura.cmd
```

Ou, manualmente:

```bash
# 1. Limpar runs anteriores (opcional, evita misturar resultados)
rmdir /s /q TestResults 2>nul
rmdir /s /q CoverageReport 2>nul

# 2. Rodar testes com cobertura (Coverlet gera XML em TestResults)
dotnet test CDB.Api.Tests\CDB.Api.Tests.csproj --settings coverlet.runsettings --collect:"XPlat Code Coverage" --results-directory ./TestResults --nologo

# 3. Gerar HTML (ReportGenerator lê o XML e gera CoverageReport)
reportgenerator -reports:"TestResults/**/coverage.cobertura.xml" -targetdir:"CoverageReport" -reporttypes:Html
```

**Visualizar:** abra no navegador o arquivo `CoverageReport\index.html`.

A métrica considera apenas **Services** e **Models**; Program, Startup, Controllers e Infrastructure são excluídos em `coverlet.runsettings`.

## Endpoints da API

- `POST /api/cdb/calcular`
  - Request: `{ "valor": 1000, "prazoMeses": 12 }`
  - Response: `{ "valorBruto": 1113.07, "valorLiquido": 1089.46, "imposto": 23.61 }`

## Configuração (alíquotas)

As alíquotas de imposto podem ser alteradas em `CDB.Api/App.config`:

```xml
<appSettings>
  <add key="AliquotaAte6Meses" value="0.225" />
  <add key="AliquotaAte12Meses" value="0.20" />
  <add key="AliquotaAte24Meses" value="0.175" />
  <add key="AliquotaAcima24Meses" value="0.15" />
</appSettings>
```

## Fórmula CDB

- **Mensal**: VF = VI × [1 + (CDI × TB)]
- **Valores fixos**: CDI = 0,9%, TB = 108%
- **Imposto**: conforme tabela até 6m (22,5%), até 12m (20%), até 24m (17,5%), acima (15%)

## Mapeamento Requisitos x Implementação

| Requisito | Implementação |
|-----------|----------------|
| Tela com valor e prazo | `CdbCalculatorComponent` |
| Exibir bruto e líquido | Template com `resultado.valorBruto`, `resultado.valorLiquido` |
| Web API com fórmula CDB | `CdbCalculator`, `CdbController` |
| Tabela de imposto | `ImpostoProvider` |
| SOLID | `ICdbCalculator`, `IImpostoProvider`, injeção de dependência |
| Cobertura > 90% | Testes na camada lógica, relatório via Coverlet + ReportGenerator |
| Angular CLI | Projeto criado com `ng new` |
