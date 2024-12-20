using Microsoft.EntityFrameworkCore;
using super_heroi_api.Data;
using super_heroi_api.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Entity Framework
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Habilite o CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // URL do front-end Angular
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Use a política CORS
app.UseCors("AllowAngularApp");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Resetar a sequência do Identity para o valor desejado
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // Resetar o contador de Identity da tabela Herois
    context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Herois', RESEED, 0);");

    // Resetar o contador de Identity da tabela Superpoderes
    context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Superpoderes', RESEED, 0);");

    // Limpar os heróis existentes
    if (context.Herois.Any())
    {
        context.Herois.RemoveRange(context.Herois);
        context.SaveChanges();
    }

    // Limpar os superpoderes existentes
    if (context.Superpoderes.Any())
    {
        context.Superpoderes.RemoveRange(context.Superpoderes);
        context.SaveChanges();
    }

    // Adicionar novos superpoderes
    var superpoderes = new List<Superpoderes>
    {
        new Superpoderes { Superpoder = "Super Força", Descricao = "Aumenta a força física do herói." },
        new Superpoderes { Superpoder = "Vôo", Descricao = "Permite ao herói voar." },
        new Superpoderes { Superpoder = "Visão de Raio-X", Descricao = "Permite enxergar através de objetos sólidos." },
        new Superpoderes { Superpoder = "Teletransporte", Descricao = "Capacidade de se teletransportar instantaneamente." },
        new Superpoderes { Superpoder = "Invisibilidade", Descricao = "Torna o herói invisível aos olhos humanos." },
        new Superpoderes { Superpoder = "Super Velocidade", Descricao = "Permite ao herói se mover mais rápido que a luz." },
        new Superpoderes { Superpoder = "Controle Mental", Descricao = "Permite controlar a mente de outras pessoas." },
        new Superpoderes { Superpoder = "Super Agilidade", Descricao = "Aumenta a agilidade e os reflexos do herói." },
        new Superpoderes { Superpoder = "Intangibilidade", Descricao = "Permite ao herói se tornar intangível, atravessando objetos." },
        new Superpoderes { Superpoder = "Telecinese", Descricao = "Permite mover objetos com a mente." },
        new Superpoderes { Superpoder = "Invulnerabilidade", Descricao = "Torna o herói imune a danos físicos." },
        new Superpoderes { Superpoder = "Manipulação do Tempo", Descricao = "Permite controlar o fluxo do tempo." },
        new Superpoderes { Superpoder = "Poder de Cura", Descricao = "Permite curar ferimentos rapidamente." },
        new Superpoderes { Superpoder = "Criar Portais", Descricao = "Permite abrir portais para outras dimensões ou locais." },
        new Superpoderes { Superpoder = "Habilidade de Controle de Elementos", Descricao = "Permite controlar os elementos da natureza (fogo, água, terra, ar)." },
        new Superpoderes { Superpoder = "Explosão de Energia", Descricao = "Permite liberar grandes rajadas de energia." },
        new Superpoderes { Superpoder = "Leitura de Mentes", Descricao = "Permite ler os pensamentos de outras pessoas." },
        new Superpoderes { Superpoder = "Absorção de Poderes", Descricao = "Permite ao herói absorver as habilidades de outros super-heróis." },
        new Superpoderes { Superpoder = "Regeneração", Descricao = "Permite regenerar tecidos e curar ferimentos em tempo real." },
        new Superpoderes { Superpoder = "Metamorfose", Descricao = "Permite transformar-se em qualquer outra forma ou pessoa." },
        new Superpoderes { Superpoder = "Poder de Persuasão", Descricao = "Permite convencer outras pessoas a fazer o que o herói deseja." },
        new Superpoderes { Superpoder = "Raio de Energia", Descricao = "Dispara rajadas concentradas de energia para destruir alvos." }
    };

    context.Superpoderes.AddRange(superpoderes);
    context.SaveChanges();

    // Adicionar heróis com superpoderes
    var herois = new List<Herois>
    {
        new Herois
        {
            Nome = "Bruce Wayne",
            NomeHeroi = "Batman",
            DataNascimento = new DateTime(1980, 5, 21),
            Altura = 1.88f,
            Peso = 95f,
            HeroisSuperpoderes = new List<HeroisSuperpoderes>
            {
                new HeroisSuperpoderes { SuperpoderId = superpoderes.First(s => s.Superpoder == "Super Força").Id },
                new HeroisSuperpoderes { SuperpoderId = superpoderes.First(s => s.Superpoder == "Vôo").Id }
            }
        },
        new Herois
        {
            Nome = "Clark Kent",
            NomeHeroi = "Superman",
            DataNascimento = new DateTime(1978, 7, 17),
            Altura = 1.91f,
            Peso = 107f,
            HeroisSuperpoderes = new List<HeroisSuperpoderes>
            {
                new HeroisSuperpoderes { SuperpoderId = superpoderes.First(s => s.Superpoder == "Super Velocidade").Id },
                new HeroisSuperpoderes { SuperpoderId = superpoderes.First(s => s.Superpoder == "Visão de Raio-X").Id }
            }
        },
        new Herois
        {
            Nome = "Diana Prince",
            NomeHeroi = "Mulher-Maravilha",
            DataNascimento = new DateTime(1970, 5, 15),
            Altura = 1.80f,
            Peso = 75f,
            HeroisSuperpoderes = new List<HeroisSuperpoderes>
            {
                new HeroisSuperpoderes { SuperpoderId = superpoderes.First(s => s.Superpoder == "Super Força").Id },
                new HeroisSuperpoderes { SuperpoderId = superpoderes.First(s => s.Superpoder == "Controle Mental").Id }
            }
        },
        new Herois
        {
            Nome = "Peter Parker",
            NomeHeroi = "Homem-Aranha",
            DataNascimento = new DateTime(1990, 8, 10),
            Altura = 1.75f,
            Peso = 70f,
            HeroisSuperpoderes = new List<HeroisSuperpoderes>
            {
                new HeroisSuperpoderes { SuperpoderId = superpoderes.First(s => s.Superpoder == "Super Agilidade").Id },
                new HeroisSuperpoderes { SuperpoderId = superpoderes.First(s => s.Superpoder == "Telecinese").Id }
            }
        },
        new Herois
        {
            Nome = "Tony Stark",
            NomeHeroi = "Homem de Ferro",
            DataNascimento = new DateTime(1975, 5, 29),
            Altura = 1.85f,
            Peso = 90f,
            HeroisSuperpoderes = new List<HeroisSuperpoderes>
            {
                new HeroisSuperpoderes { SuperpoderId = superpoderes.First(s => s.Superpoder == "Invulnerabilidade").Id },
                new HeroisSuperpoderes { SuperpoderId = superpoderes.First(s => s.Superpoder == "Manipulação do Tempo").Id }
            }
        }
    };

    context.Herois.AddRange(herois);
    context.SaveChanges();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
