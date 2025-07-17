# ParkManager - Sistema de Gerenciamento de Estacionamento

## Descrição
Sistema completo de gerenciamento de estacionamento desenvolvido em .NET 8 com Entity Framework Core e PostgreSQL, implementando Clean Architecture com separação de responsabilidades.

## Arquitetura

### Camadas do Projeto
- **ParkManager.Domain**: Entidades de domínio e interfaces
- **ParkManager.Infrastructure**: Implementação de repositórios e contexto de dados
- **ParkManager.Application**: Lógica de aplicação (camada de serviços)
- **ParkManager.API**: Controllers e endpoints da API REST

### Tecnologias Utilizadas
- .NET 8.0
- ASP.NET Core Web API
- Entity Framework Core 8.0
- PostgreSQL
- AutoMapper
- Swagger/OpenAPI

## Decisões Técnicas

### Escolha do ORM
**Entity Framework Core** foi escolhido como ORM principal pelas seguintes razões:
- **Integração nativa**: Integração perfeita com .NET 8 e ASP.NET Core
- **Code First**: Permite definir o modelo de dados através de classes C#
- **Migrations**: Sistema robusto de versionamento de banco de dados
- **Performance**: Otimizações automáticas de consultas e lazy loading
- **Ecosistema**: Ampla documentação e suporte da comunidade

### Estrutura da API
**Clean Architecture** foi implementada para:
- **Separação de responsabilidades**: Cada camada tem uma responsabilidade específica
- **Testabilidade**: Facilita a criação de testes unitários e de integração
- **Manutenibilidade**: Código mais organizado e fácil de manter
- **Escalabilidade**: Permite crescimento sustentável da aplicação

### Estratégias de Tratamento de Erros
- **Middleware Global**: Intercepta todas as exceções e retorna respostas padronizadas
- **Validações de Modelo**: Data Annotations para validação de entrada
- **Logs Estruturados**: Logging detalhado para rastreamento de erros
- **Códigos HTTP Apropriados**: Respostas semânticas (200, 201, 400, 404, 500)

### Banco de Dados PostgreSQL
**PostgreSQL** foi escolhido como banco de dados por:
- **Performance**: Excelente performance em consultas complexas
- **Confiabilidade**: Sistema robusto com suporte a transações ACID
- **Escalabilidade**: Capacidade de crescer com o sistema
- **Padrão da Indústria**: Amplamente usado em sistemas corporativos
- **Código Aberto**: Sem custos de licenciamento

### Arquitetura de Upload CSV
- **Processamento Assíncrono**: Upload não bloqueia outras operações
- **Validação Robusta**: Validação linha por linha com relatório de erros
- **Tratamento de Erros**: Falhas individuais não interrompem o processamento
- **Logging Detalhado**: Rastreamento completo do processamento

## Estrutura de Dados

### Entidades
- **Cliente**: Gerencia informações dos clientes
- **Veículo**: Gerencia informações dos veículos
- **Mensalista**: Gerencia contratos de mensalidade
- **Faturamento**: Gerencia cobrança mensal dos mensalistas

### Relacionamentos
- Um Cliente pode ter múltiplos Veículos (1:N)
- Um Mensalista está associado a um Cliente e um Veículo (1:1)
- Um Mensalista pode ter múltiplos Faturamentos (1:N)

## Funcionalidades

### API Endpoints

#### Clientes
- `GET /api/cliente` - Listar todos os clientes
- `GET /api/cliente/{id}` - Obter cliente por ID
- `POST /api/cliente` - Criar novo cliente
- `PUT /api/cliente/{id}` - Atualizar cliente
- `DELETE /api/cliente/{id}` - Excluir cliente

#### Veículos
- `GET /api/veiculo` - Listar todos os veículos
- `GET /api/veiculo/{id}` - Obter veículo por ID
- `GET /api/veiculo/placa/{placa}` - Obter veículo por placa
- `GET /api/veiculo/cliente/{clienteId}` - Obter veículos por cliente
- `POST /api/veiculo` - Criar novo veículo
- `PUT /api/veiculo/{id}` - Atualizar veículo
- `DELETE /api/veiculo/{id}` - Excluir veículo

#### Mensalistas
- `GET /api/mensalista` - Listar todos os mensalistas
- `GET /api/mensalista/ativos` - Listar mensalistas ativos
- `GET /api/mensalista/{id}` - Obter mensalista por ID
- `GET /api/mensalista/cliente/{clienteId}` - Obter mensalista por cliente
- `POST /api/mensalista` - Criar novo mensalista
- `PUT /api/mensalista/{id}` - Atualizar mensalista
- `DELETE /api/mensalista/{id}` - Excluir mensalista

#### Faturamento (Em Desenvolvimento)
- Sistema de faturamento mensal para mensalistas
- Geração automática de cobrança
- Controle de pagamentos
- Relatórios de faturamento

#### Upload CSV (Em Desenvolvimento)
- Upload de arquivo CSV para cadastro em massa de veículos
- Validação e processamento linha por linha
- Relatório de erros detalhado
- Template de arquivo disponível

## Configuração do Ambiente

### Pré-requisitos
- .NET 8.0 SDK
- PostgreSQL 15+ ou Docker
- Visual Studio Code ou Visual Studio

### Configuração do Banco de Dados
1. Instalar PostgreSQL localmente ou usar Docker:
```bash
docker run --name postgres-parkmanager -e POSTGRES_DB=ParkManagerDb -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=postgres -p 5432:5432 -d postgres:15
```

2. Configurar a string de conexão no `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=ParkManagerDb;Username=postgres;Password=postgres;Port=5432"
  }
}
```

3. Executar as migrations:
```bash
cd ParkManager.Infrastructure
dotnet ef database update
```

### Executar o Projeto
```bash
cd ParkManager.API
dotnet run
```

A API estará disponível em: `http://localhost:5154`

### Acessar Swagger
Abra o navegador e acesse: `http://localhost:5154/swagger`

## Validações Implementadas

### Cliente
- Nome é obrigatório (máximo 100 caracteres)
- Telefone é obrigatório (máximo 20 caracteres)
- Telefone único no sistema

### Veículo
- Placa é obrigatória (máximo 8 caracteres)
- Modelo é obrigatório (máximo 50 caracteres)
- ClienteId é obrigatório
- Placa única no sistema

### Mensalista
- ClienteId é obrigatório
- VeiculoId é obrigatório
- Data de início é obrigatória
- Data de vencimento é obrigatória
- Valor é obrigatório (deve ser maior que zero)

### Faturamento
- MesAno deve ser informado no formato YYYYMM (exemplo: 202501 para janeiro de 2025)
- Valor deve ser maior que zero
- Data de vencimento é obrigatória
- Sistema não permite faturamento duplicado para o mesmo mensalista no mesmo mês

### Upload CSV
- Arquivo deve ter extensão .csv
- Formato esperado: Placa,Modelo,TelefoneCliente
- Placa deve ser única no sistema
- Cliente deve existir no sistema (busca pelo telefone)
- Processamento linha por linha com relatório de erros detalhado

## Estrutura de Resposta

### Exemplo de Cliente
```json
{
  "id": "guid",
  "nome": "João Silva",
  "telefone": "(11) 99999-9999",
  "veiculos": [
    {
      "id": "guid",
      "placa": "ABC1234",
      "modelo": "Civic",
      "clienteId": "guid"
    }
  ]
}
```

### Exemplo de Faturamento
```json
{
  "id": "guid",
  "mensalistaId": "guid",
  "mesAno": 202501,
  "valor": 150.00,
  "dataVencimento": "2025-01-31T00:00:00",
  "dataGeracao": "2025-01-01T10:00:00",
  "dataPagamento": null,
  "pago": false,
  "observacoes": null,
  "mensalista": {
    "cliente": {
      "nome": "João Silva",
      "telefone": "(11) 99999-9999"
    },
    "veiculo": {
      "placa": "ABC1234",
      "modelo": "Civic"
    }
  }
}
```

### Exemplo de Upload CSV
Formato do arquivo CSV:
```csv
Placa,Modelo,TelefoneCliente
ABC1234,Honda Civic,(11) 99999-9999
XYZ5678,Toyota Corolla,(11) 88888-8888
```

Resposta do upload:
```json
{
  "totalLinhas": 2,
  "veiculosProcessados": 2,
  "veiculosComErro": 0,
  "erros": [],
  "veiculosCriados": [
    {
      "placa": "ABC1234",
      "modelo": "Honda Civic",
      "telefoneCliente": "(11) 99999-9999"
    }
  ]
}
```

### Exemplo de Mensalista
```json
{
  "id": "guid",
  "clienteId": "guid",
  "veiculoId": "guid",
  "dataInicio": "2024-01-01T00:00:00",
  "dataVencimento": "2024-02-01T00:00:00",
  "valor": 150.00,
  "ativo": true,
  "cliente": {
    "id": "guid",
    "nome": "João Silva",
    "telefone": "(11) 99999-9999"
  },
  "veiculo": {
    "id": "guid",
    "placa": "ABC1234",
    "modelo": "Civic",
    "clienteId": "guid"
  }
}
```

## Testes

### Arquivo de Testes HTTP
O projeto inclui um arquivo `ParkManager.API.http` com exemplos de requisições para todos os endpoints.

### Testando com Swagger
1. Execute o projeto
2. Acesse `http://localhost:5154/swagger`
3. Utilize a interface do Swagger para testar os endpoints

## Considerações Técnicas

### Padrões Implementados
- Clean Architecture
- Repository Pattern
- Dependency Injection
- Data Transfer Objects (DTOs)
- AutoMapper para mapeamento
- Validações com Data Annotations
- Middleware para tratamento global de erros
- Service Layer para validações de negócio
- CORS configurado

### Tratamento de Erros
- Middleware global de exceções
- Validação de modelos
- Verificação de duplicatas
- Tratamento de recursos não encontrados
- Respostas HTTP apropriadas
- Logs estruturados

### Segurança
- Validação de entrada
- Prevenção de referências circulares
- Sanitização de dados
- CORS configurado para desenvolvimento

### Validações de Negócio
- Validação de existência de entidades relacionadas
- Validação de relacionamentos (veículo pertence ao cliente)
- Validação de regras de negócio (cliente único para mensalista)
- Validação de datas (vencimento posterior ao início)

## Melhorias Futuras
- Implementar autenticação e autorização
- Adicionar logs estruturados avançados
- Implementar cache (Redis)
- Adicionar testes unitários e de integração
- Implementar paginação nos endpoints
- Adicionar métricas e monitoramento
- Implementar versionamento da API
- Adicionar documentação OpenAPI mais detalhada
- Implementar rate limiting
- Adicionar suporte a múltiplos ambientes

### Eficiência e Robustez do Upload CSV
- **Validação Incremental**: Processamento linha por linha sem interrupção total
- **Relatório Detalhado**: Erros específicos para cada linha processada
- **Transações Otimizadas**: Cada veículo é salvo individualmente para evitar rollback total
- **Template Disponível**: Endpoint para download do formato correto
- **Logging Completo**: Rastreamento detalhado do processamento

### Eficiência na Manipulação do PostgreSQL
- **Índices Otimizados**: Índices únicos em telefone e placa para consultas rápidas
- **Relacionamentos Eficientes**: Foreign keys com DeleteBehavior.Restrict para integridade
- **Consultas Otimizadas**: Include statements para evitar N+1 queries
- **Connection Pooling**: Configuração padrão do Entity Framework para reuso de conexões

## Status da Implementação

### ✅ Implementado e Funcional
1. **Cadastro de clientes** - CRUD completo com Nome e Telefone obrigatórios
2. **Cadastro de veículos** - CRUD completo com Placa e Modelo obrigatórios  
3. **Associação múltipla** - Um cliente pode ter vários veículos
4. **Gerenciamento de mensalistas** - CRUD completo com validações de negócio
5. **Migração para PostgreSQL** - Banco de dados configurado e funcionando
6. **API REST documentada** - Endpoints funcionais com Swagger
7. **Validações de negócio** - Regras implementadas e testadas
8. **Tratamento de erros** - Middleware global implementado

### 🚧 Em Desenvolvimento
1. **Faturamento mensal** - Estrutura criada, implementação em progresso
2. **Upload CSV** - Funcionalidade planejada e estrutura básica definida

### 🔄 Próximos Passos
1. Finalizar implementação do sistema de faturamento
2. Implementar upload e processamento de CSV
3. Criar testes unitários
4. Adicionar logging avançado
5. Implementar autenticação e autorização

## Recursos Adicionais Implementados
- **Middleware de Exceções**: Tratamento global de erros com logging
- **Validações de Negócio**: Serviço dedicado para validações complexas
- **CORS**: Configurado para desenvolvimento
- **Configuração Docker**: Docker Compose para deployment
- **Dockerfile**: Containerização da aplicação

## Estrutura Avançada
O projeto inclui agora:
- Middleware personalizado para tratamento de erros
- Serviços de validação de regras de negócio
- Configuração de CORS
- Estrutura preparada para testes
- Configuração para containerização
