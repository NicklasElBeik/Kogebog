using Kogebog.API.Repository.Database.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kogebog.API.Repository.Database
{
    public static class BuildExtensions
    {
        public static void SeedDatabase(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            if (!dbContext.Units.Any())
            {
                dbContext.Units.AddRange(
                    new Unit { Name = "kilogram", PluralName = "kilogram", Abbreviation = "kg" },
                    new Unit { Name = "gram", PluralName = "gram", Abbreviation = "g" },
                    new Unit { Name = "liter", PluralName = "liter", Abbreviation = "l" },
                    new Unit { Name = "deciliter", PluralName = "deciliter", Abbreviation = "dl" },
                    new Unit { Name = "centiliter", PluralName = "centiliter", Abbreviation = "cl" },
                    new Unit { Name = "milliliter", PluralName = "mililiter", Abbreviation = "ml" },
                    new Unit { Name = "teskefuld", PluralName = "teskefulde", Abbreviation = "tsk" },
                    new Unit { Name = "spiseskefuld", PluralName = "spiseskefulde", Abbreviation = "spsk" },
                    new Unit { Name = "kop", PluralName = "koppe", Abbreviation = "kop" },
                    new Unit { Name = "stykke", PluralName = "stykker", Abbreviation = "stk" },
                    new Unit { Name = "dryp", PluralName = "dryp", Abbreviation = "dryp" },
                    new Unit { Name = "stang", PluralName = "stænger", Abbreviation = "stang" },
                    new Unit { Name = "fed", PluralName = "fed", Abbreviation = "fed" },
                    new Unit { Name = "skive", PluralName = "skiver", Abbreviation = "skive" },
                    new Unit { Name = "hoved", PluralName = "hoveder", Abbreviation = "hoved" },
                    new Unit { Name = "buket", PluralName = "buketter", Abbreviation = "buket" },
                    new Unit { Name = "stilk", PluralName = "stilke", Abbreviation = "stilk" });

                dbContext.SaveChanges();
            }

            if (!dbContext.Ingredients.Any())
            {
                dbContext.Ingredients.AddRange(
                    // **Krydderier & Smagsstoffer**
                    new Ingredient { Name = "Salt" },
                    new Ingredient { Name = "Sort peber" },
                    new Ingredient { Name = "Hvid peber" },
                    new Ingredient { Name = "Paprika" },
                    new Ingredient { Name = "Cayennepeber" },
                    new Ingredient { Name = "Chilipulver" },
                    new Ingredient { Name = "Spidskommen" },
                    new Ingredient { Name = "Koriander" },
                    new Ingredient { Name = "Gurkemeje" },
                    new Ingredient { Name = "Kanel" },
                    new Ingredient { Name = "Muskatnød" },
                    new Ingredient { Name = "Nelliker" },
                    new Ingredient { Name = "Allehånde" },
                    new Ingredient { Name = "Ingefær" },
                    new Ingredient { Name = "Sennepspulver" },
                    new Ingredient { Name = "Laurbærblade" },
                    new Ingredient { Name = "Safran" },
                    new Ingredient { Name = "Kardemomme" },
                    new Ingredient { Name = "Bukkehornsfrø" },
                    new Ingredient { Name = "Fennikelfrø" },
                    new Ingredient { Name = "Sellerisalt" },

                    // **Urter**
                    new Ingredient { Name = "Basilikum" },
                    new Ingredient { Name = "Oregano" },
                    new Ingredient { Name = "Timian" },
                    new Ingredient { Name = "Rosmarin" },
                    new Ingredient { Name = "Salvie" },
                    new Ingredient { Name = "Dild" },
                    new Ingredient { Name = "Persille" },
                    new Ingredient { Name = "Mynte" },
                    new Ingredient { Name = "Purløg" },
                    new Ingredient { Name = "Estragon" },
                    new Ingredient { Name = "Koriander" },

                    // **Grøntsager**
                    new Ingredient { Name = "Løg" },
                    new Ingredient { Name = "Hvidløg" },
                    new Ingredient { Name = "Skalotteløg" },
                    new Ingredient { Name = "Forårsløg" },
                    new Ingredient { Name = "Porrer" },
                    new Ingredient { Name = "Tomater" },
                    new Ingredient { Name = "Gulerødder" },
                    new Ingredient { Name = "Selleri" },
                    new Ingredient { Name = "Peberfrugter" },
                    new Ingredient { Name = "Jalapeñopeber" },
                    new Ingredient { Name = "Champignoner" },
                    new Ingredient { Name = "Squash" },
                    new Ingredient { Name = "Aubergine" },
                    new Ingredient { Name = "Hvidkål" },
                    new Ingredient { Name = "Grønkål" },
                    new Ingredient { Name = "Spinat" },
                    new Ingredient { Name = "Salat" },
                    new Ingredient { Name = "Broccoli" },
                    new Ingredient { Name = "Blomkål" },
                    new Ingredient { Name = "Asparges" },
                    new Ingredient { Name = "Majs" },
                    new Ingredient { Name = "Agurk" },
                    new Ingredient { Name = "Radiser" },
                    new Ingredient { Name = "Rødbeder" },
                    new Ingredient { Name = "Græskar" },
                    new Ingredient { Name = "Søde kartofler" },
                    new Ingredient { Name = "Kartofler" },

                    // **Kød & Fjerkræ**
                    new Ingredient { Name = "Kyllingebryst" },
                    new Ingredient { Name = "Kyllingelår" },
                    new Ingredient { Name = "Hakket kylling" },
                    new Ingredient { Name = "Kyllingevinger" },
                    new Ingredient { Name = "Kyllingelårfilet" },
                    new Ingredient { Name = "Hel kylling" },
                    new Ingredient { Name = "Oksebøf" },
                    new Ingredient { Name = "Hakket oksekød" },
                    new Ingredient { Name = "Oksekødsribben" },
                    new Ingredient { Name = "Oksebryst" },
                    new Ingredient { Name = "Lamme koteletter" },
                    new Ingredient { Name = "Hakket lam" },
                    new Ingredient { Name = "Svinekoteletter" },
                    new Ingredient { Name = "Hakket svinekød" },
                    new Ingredient { Name = "Svineslag" },
                    new Ingredient { Name = "Bacon" },
                    new Ingredient { Name = "Skinke" },
                    new Ingredient { Name = "Pølser" },

                    // **Fisk & Skaldyr**
                    new Ingredient { Name = "Laks" },
                    new Ingredient { Name = "Tun" },
                    new Ingredient { Name = "Torsk" },
                    new Ingredient { Name = "Sej" },
                    new Ingredient { Name = "Rejer" },
                    new Ingredient { Name = "Hummer" },
                    new Ingredient { Name = "Krabbe" },
                    new Ingredient { Name = "Muslinger" },
                    new Ingredient { Name = "Østers" },
                    new Ingredient { Name = "Kammuslinger" },
                    new Ingredient { Name = "Blæksprutte" },

                    // **Mejeriprodukter & Æg**
                    new Ingredient { Name = "Mælk" },
                    new Ingredient { Name = "Piskefløde" },
                    new Ingredient { Name = "Cheddarost" },
                    new Ingredient { Name = "Mozzarellaost" },
                    new Ingredient { Name = "Parmesanost" },
                    new Ingredient { Name = "Blåskimmelost" },
                    new Ingredient { Name = "Flødeost" },
                    new Ingredient { Name = "Yoghurt" },
                    new Ingredient { Name = "Smør" },
                    new Ingredient { Name = "Æg" },

                    // **Korn & Bælgfrugter**
                    new Ingredient { Name = "Ris" },
                    new Ingredient { Name = "Pasta" },
                    new Ingredient { Name = "Couscous" },
                    new Ingredient { Name = "Quinoa" },
                    new Ingredient { Name = "Linser" },
                    new Ingredient { Name = "Kikærter" },
                    new Ingredient { Name = "Sortebønner" },
                    new Ingredient { Name = "Kidneybønner" },
                    new Ingredient { Name = "Havregryn" },
                    new Ingredient { Name = "Majsmel" },
                    new Ingredient { Name = "Brød" },
                    new Ingredient { Name = "Tortilla" },

                    // **Olier & Dressinger**
                    new Ingredient { Name = "Olivenolie" },
                    new Ingredient { Name = "Vegetabilsk olie" },
                    new Ingredient { Name = "Sojasauce" },
                    new Ingredient { Name = "Eddike" },
                    new Ingredient { Name = "Æblecidereddike" },
                    new Ingredient { Name = "Balsamicoeddike" },
                    new Ingredient { Name = "Worcestershire sauce" },
                    new Ingredient { Name = "Chilisauce" },
                    new Ingredient { Name = "Ketchup" },
                    new Ingredient { Name = "Mayonnaise" },
                    new Ingredient { Name = "Sennep" },
                    new Ingredient { Name = "Ahornsirup" },
                    new Ingredient { Name = "Honning" },

                    // **Nødder & Frø**
                    new Ingredient { Name = "Mandler" },
                    new Ingredient { Name = "Jordnødder" },
                    new Ingredient { Name = "Cashewnødder" },
                    new Ingredient { Name = "Valnødder" },
                    new Ingredient { Name = "Pekannødder" },
                    new Ingredient { Name = "Solsikkekerner" },
                    new Ingredient { Name = "Chiafrø" },
                    new Ingredient { Name = "Hørfrø" },

                    // **Bageingredienser**
                    new Ingredient { Name = "Hvedemel" },
                    new Ingredient { Name = "Bagepulver" },
                    new Ingredient { Name = "Natron" },
                    new Ingredient { Name = "Gær" },
                    new Ingredient { Name = "Majsstivelse" },
                    new Ingredient { Name = "Kakaopulver" },
                    new Ingredient { Name = "Chokoladestykker" },
                    new Ingredient { Name = "Vaniljeekstrakt" });

                dbContext.SaveChanges();
            }
        }
    }
}
