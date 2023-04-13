using HakanBoardGames.Shared.BoardGameModels;
using Microsoft.EntityFrameworkCore;

namespace MudBlazorVisualDNA.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // exempel Fluent Api

            //var familyTree = modelBuilder.Entity<FTFamilyTree>();
            //familyTree.HasKey(b => b.FamilyTreeId);
            //familyTree.Property(b => b.FamilyTreeId).UseIdentityColumn();
            //familyTree.HasMany(t => t.Haplogroups)
            //    .WithOne(c => c.FamilyTree)
            //    .HasForeignKey(c => c.FamilyTreeId)
            //    .OnDelete(DeleteBehavior.Cascade);
            //familyTree.HasMany(t => t.Countries)
            //    .WithOne(c => c.FamilyTree)
            //    .HasForeignKey(c => c.FamilyTreeId)
            //    .OnDelete(DeleteBehavior.Cascade);

            //var haplogroup = modelBuilder.Entity<FTHaplogroup>();
            //haplogroup.HasKey(b => b.HaplogroupId);
            //haplogroup.Property(b => b.HaplogroupId).UseIdentityColumn();
            //haplogroup.HasMany(t => t.CountryCounts)
            //   .WithOne(c => c.Haplogroup)
            //   .HasForeignKey(c => c.FTCountryCountId)
            //   .OnDelete(DeleteBehavior.Cascade);
            //haplogroup.HasMany(t => t.Variants)
            //    .WithOne(c => c.Haplogroup)
            //    .HasForeignKey(c => c.FTVariantId)
            //    .OnDelete(DeleteBehavior.Cascade);
            //haplogroup.HasMany(t => t.Surnames)
            //    .WithOne(c => c.Haplogroup)
            //    .HasForeignKey(c => c.FTSurnameId)
            //    .OnDelete(DeleteBehavior.Cascade);
            //haplogroup.HasMany(t => t.Children)
            //    .WithOne(c => c.Haplogroup)
            //    .HasForeignKey(c => c.FTChildId)
            //    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<FTCountryCount>()
            //  .HasOne(p => p.Haplogroup)
            //  .WithMany(b => b.CountryCounts)
            //  .HasForeignKey(p => p.HaplogroupId)
            //  .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<FTVariant>()
            //  .HasOne(p => p.Haplogroup)
            //  .WithMany(b => b.Variants)
            //  .HasForeignKey(p => p.HaplogroupId)
            //  .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<FTSurname>()
            //  .HasOne(p => p.Haplogroup)
            //  .WithMany(b => b.Surnames)
            //  .HasForeignKey(p => p.HaplogroupId)
            //  .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<FTChild>()
            //  .HasOne(p => p.Haplogroup)
            //  .WithMany(b => b.Children)
            //  .HasForeignKey(p => p.HaplogroupId)
            //  .OnDelete(DeleteBehavior.Cascade);

        }

        public DbSet<BGBoardGame>? BoardGames { get; set; }

        public DbSet<BGPlayer>? Players { get; set; }
        public DbSet<BGCreator>? Creators { get; set; }
        public DbSet<BGCategory>? Categories { get; set; }
    }
}

/*
[
  {
    "id": "1",
    "name": "Gloomhaven",
    "released": "2017",
    "tagline": "Vanquish monsters with strategic cardplay. Fulfill your quest to leave your legacy!",
    "players": {
      "min": 1,
      "max": 4
    },
    "age": 14,
    "creators": [
      "Isaac Childres"
    ],
    "categories": [
      "Adventure",
      "Exploration",
      "Fantasy",
      "Fighting",
      "Miniatures"
    ]
  },
  {
    "id": "2",
    "name": "Pandemic Legacy: Season 1",
    "released": "2015",
    "tagline": "Mutating diseases are spreading around the world - can your team save humanity?",
    "players": {
      "min": 2,
      "max": 4
    },
    "age": 13,
    "creators": [
      "Rob Daviau",
      "Matt Leacock"
    ],
    "categories": [
      "Environmental",
      "Medical"
    ]
  },
  {
    "id": "3",
    "name": "Brass: Birmingham",
    "released": "2018",
    "tagline": "Build networks, grow industries, and navigate the world of the Industrial Revolution.",
    "players": {
      "min": 2,
      "max": 4
    },
    "age": 14,
    "creators": [
      "Gavan Brown",
      "Matt Tolman",
      "Martin Wallace"
    ],
    "categories": [
      "Economic",
      "Industry / Manufacturing",
      "Transportation"
    ]
  },
  {
    "id": "4",
    "name": "Terraforming Mars",
    "released": "2016",
    "tagline": "Compete with rival CEOs to make Mars habitable and build your corporate empire.",
    "players": {
      "min": 1,
      "max": 5
    },
    "age": 12,
    "creators": [
      "Jacob Fryxelius"
    ],
    "categories": [
      "Economic",
      "Environmental",
      "Industry / Manufacturing",
      "Science Fiction",
      "Space Exploration",
      "Territory Building"
    ]
  },
  {
    "id": "5",
    "name": "Twilight Imperium: Fourth Edition",
    "released": "2017",
    "tagline": "Build an intergalactic empire through trade, research, conquest and grand politics.",
    "players": {
      "min": 3,
      "max": 6
    },
    "age": 14,
    "creators": [
      "Dane Beltrami",
      "Corey Konieczka",
      "Christian T. Petersen"
    ],
    "categories": [
      "Civilization",
      "Economic",
      "Exploration",
      "Negotiation",
      "Political",
      "Science Fiction",
      "Space Exploration",
      "Wargame"
    ]
  },
  {
    "id": "6",
    "name": "Gaia Project",
    "released": "2017",
    "tagline": "Expand, research, upgrade, and settle the galaxy with one of 14 alien species. ",
    "players": {
      "min": 1,
      "max": 4
    },
    "age": 12,
    "creators": [
      "Jens Drögemüller",
      "Helge Ostertag"
    ],
    "categories": [
      "Economic",
      "Science Fiction",
      "Space Exploration",
      "Territory Building"
    ]
  },
  {
    "id": "7",
    "name": "Star Wars: Rebellion",
    "released": "2016",
    "tagline": "Strike from your hidden base as the Rebels—or find and destroy it as the Empire.",
    "players": {
      "min": 2,
      "max": 4
    },
    "age": 14,
    "creators": [
      "Corey Konieczka"
    ],
    "categories": [
      "Civil War",
      "Fighting",
      "Miniatures",
      "Movies / TV / Radio theme",
      "Science Fiction",
      "Wargame"
    ]
  },
  {
    "id": "8",
    "name": "Through the Ages: A New Story of Civilization",
    "released": "2015",
    "tagline": "Rewrite history as you build up your civilization in this epic card drafting game!",
    "players": {
      "min": 2,
      "max": 4
    },
    "age": 14,
    "creators": [
      "Vlaada Chvátil"
    ],
    "categories": [
      "Card Game",
      "Civilization",
      "Economic"
    ]
  },
  {
    "id": "9",
    "name": "Great Western Trail",
    "released": "2016",
    "tagline": "Use strategic outposts and navigate danger as you herd your cattle to Kansas City.",
    "players": {
      "min": 2,
      "max": 4
    },
    "age": 12,
    "creators": [
      "Alexander Pfister"
    ],
    "categories": [
      "American West",
      "Animals",
      "Economic"
    ]
  },
  {
    "id": "10",
    "name": "Spirit Island",
    "released": "2017",
    "tagline": "Island Spirits join forces using elemental powers to defend their home from invaders.",
    "players": {
      "min": 1,
      "max": 4
    },
    "age": 13,
    "creators": [
      "R. Eric Reuss"
    ],
    "categories": [
      "Age of Reason",
      "Environmental",
      "Fantasy",
      "Fighting",
      "Mythology",
      "Renaissance",
      "Territory Building"
    ]
  },
  {
    "id": "11",
    "name": "War of the Ring: Second Edition",
    "released": "2012",
    "tagline": "The Fellowship and the Free Peoples clash with Sauron over the fate of Middle-Earth.",
    "players": {
      "min": 2,
      "max": 4
    },
    "age": 13,
    "creators": [
      "Roberto Di Meglio",
      "Marco Maggi",
      "Francesco Nepitello"
    ],
    "categories": [
      "Adventure",
      "Fantasy",
      "Fighting",
      "Miniatures",
      "Novel-based",
      "Territory Building",
      "Wargame"
    ]
  },
  {
    "id": "12",
    "name": "Twilight Struggle",
    "released": "2005",
    "tagline": "Relive the Cold War and rewrite history in an epic clash between the USA and USSR.",
    "players": {
      "min": 2,
      "max": 2
    },
    "age": 13,
    "creators": [
      "Ananda Gupta",
      "Jason Matthews"
    ],
    "categories": [
      "Modern Warfare",
      "Political",
      "Wargame"
    ]
  },
  {
    "id": "13",
    "name": "Scythe",
    "released": "2016",
    "tagline": "Five factions vie for dominance in a war-torn, mech-filled, dieselpunk 1920s Europe.",
    "players": {
      "min": 1,
      "max": 5
    },
    "age": 14,
    "creators": [
      "Jamey Stegmaier"
    ],
    "categories": [
      "Economic",
      "Fighting",
      "Miniatures",
      "Science Fiction",
      "Territory Building"
    ]
  },
  {
    "id": "14",
    "name": "The Castles of Burgundy",
    "released": "2011",
    "tagline": "Plan, trade, and build your Burgundian estate to prosperity and prominence.",
    "players": {
      "min": 2,
      "max": 4
    },
    "age": 12,
    "creators": [
      "Stefan Feld"
    ],
    "categories": [
      "Dice",
      "Medieval",
      "Territory Building"
    ]
  },
  {
    "id": "15",
    "name": "Die Macher",
    "released": "1986",
    "tagline": "Players represent political parties attempting to gain power in Germany.",
    "players": {
      "min": 3,
      "max": 5
    },
    "age": 14,
    "creators": [
      "Karl-Heinz Schmiel"
    ],
    "categories": [
      "Economic",
      "Negotiation",
      "Political"
    ]
  }
]
*/