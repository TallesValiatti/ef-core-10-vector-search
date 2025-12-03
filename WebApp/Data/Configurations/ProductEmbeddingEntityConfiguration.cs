using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Data.Configurations;

public static class BookConfiguration
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>()
            .ToTable("Books");
        
        modelBuilder.Entity<Book>()
            .HasKey(x => x.Id);
        
        modelBuilder.Entity<Book>()
            .Property(x => x.Id)
            .HasMaxLength(50)
            .IsRequired();
        
        modelBuilder.Entity<Book>()
            .Property(x => x.Name)
            .HasMaxLength(500)
            .IsRequired();
        
        modelBuilder.Entity<Book>()
            .Property(x => x.Description)
            .IsRequired();
        
        modelBuilder.Entity<Book>()
            .Property(b => b.Embedding)
            .HasColumnType("vector(3072)");
    }
    
    public static List<Book> GetSeedData()
    {
        var books = new List<Book>
        {
            new()
            {
                Id = "book-001",
                Name = "The Lord of the Rings: The Fellowship of the Ring",
                Description = @"The Fellowship of the Ring is the first volume of J.R.R. Tolkien's epic high fantasy novel The Lord of the Rings. Set in Middle-earth, the story follows the hobbit Frodo Baggins as he and the Fellowship embark on a quest to destroy the One Ring and defeat the dark lord Sauron. This masterpiece of fantasy literature has captivated readers for generations with its richly detailed world-building, complex characters, and timeless themes of friendship, courage, and the struggle between good and evil.

The story begins in the peaceful Shire, where Frodo inherits a mysterious ring from his uncle Bilbo. Guided by the wizard Gandalf, Frodo learns that this is the One Ring, forged by Sauron to control all other rings of power. With his loyal companions Sam, Merry, and Pippin, Frodo must leave the safety of the Shire and journey to Rivendell, where the Council of Elrond decides the ring must be destroyed in the fires of Mount Doom.

The Fellowship is formed, consisting of Frodo and Sam, Merry and Pippin, Gandalf the wizard, Aragorn the ranger, Boromir of Gondor, Legolas the elf, and Gimli the dwarf. Together they face numerous perils including the treacherous mines of Moria, where they encounter the Balrog and lose Gandalf to the darkness. The company travels through the enchanted forest of Lothlórien, where they receive gifts from the Lady Galadriel, before continuing down the river Anduin.

Tolkien's masterful prose brings Middle-earth to life with vivid descriptions of landscapes, cultures, and languages. The book explores themes of power and corruption, the importance of fellowship and loyalty, the hero's journey, and the eternal struggle between light and darkness. The rich mythology, including the history of the rings, the ancient kingdoms, and the various races and their conflicts, creates a fully realized secondary world that has influenced countless fantasy works. The Fellowship of the Ring is not just an adventure story, but a profound meditation on courage, sacrifice, and hope in the face of overwhelming darkness."
            },
            new()
            {
                Id = "book-002",
                Name = "The Shining",
                Description = @"The Shining is a horror novel by Stephen King, first published in 1977. It tells the story of Jack Torrance, an aspiring writer and recovering alcoholic who accepts a position as the off-season caretaker of the historic Overlook Hotel in the Colorado Rockies. His five-year-old son Danny possesses psychic abilities, known as 'the shining,' which allows him to see the hotel's horrific past and terrible future.

As winter sets in and the family becomes snowbound and isolated, the supernatural forces inhabiting the hotel begin to exert their influence. Jack's sanity deteriorates as the malevolent spirits of the Overlook manipulate his mind, feeding on his anger, resentment, and buried demons. The hotel itself becomes a character in the story, a malignant presence with a dark history of violence and death. Previous caretaker Delbert Grady murdered his family at the hotel, and now the spirits want Jack to do the same.

Danny's psychic abilities make him particularly vulnerable to the hotel's evil influence. He begins experiencing terrifying visions and encounters the ghosts of previous guests and employees, including the decomposing woman in Room 217 and the two murdered Grady daughters. Dick Hallorann, the hotel's chef who also possesses the shining, warns Danny about the dangers lurking in the hotel and tells him to call for help if needed. As Jack descends further into madness, Danny and his mother Wendy must fight for their survival.

King masterfully builds tension and dread throughout the novel, exploring themes of addiction, domestic violence, isolation, and the cyclical nature of abuse. The claustrophobic setting of the snowbound hotel creates an oppressive atmosphere where escape is impossible. The novel delves deep into Jack's psyche, showing how the hotel exploits his weaknesses—his alcoholism, his frustration with his writing career, his resentment toward his family, and his own history of being abused by his father. The Shining is not just a ghost story but a psychological horror that examines how the past haunts the present and how inherited trauma can perpetuate violence across generations."
            },
            new()
            {
                Id = "book-003",
                Name = "Salt, Fat, Acid, Heat: Mastering the Elements of Good Cooking",
                Description = @"Salt, Fat, Acid, Heat by Samin Nosrat is a groundbreaking cookbook that distills the fundamental elements of good cooking into four simple components. This beautifully illustrated guide teaches readers how to use salt, fat, acid, and heat to transform basic ingredients into extraordinary meals. Rather than focusing on recipes, Nosrat empowers home cooks to understand the principles behind cooking, enabling them to cook intuitively and confidently without relying solely on written instructions.

The book is divided into four sections, each exploring one of the essential elements. Salt enhances flavor and affects texture in profound ways—it can make food taste more like itself, preserve ingredients, and transform textures in everything from vegetables to meats. Fat carries flavor, creates texture, and provides richness, whether it's olive oil, butter, or animal fats. Acid brightens dishes and balances richness, from citrus and vinegar to wine and fermented foods. Heat determines texture and affects flavor development through various cooking methods like roasting, braising, and grilling.

Nosrat draws on her training at Chez Panisse and years of teaching to present complex culinary concepts in an accessible, engaging manner. She includes charts, diagrams, and illustrations that make scientific principles easy to understand. The book features recipes that demonstrate how to apply these elements, from simple roasted vegetables to complex braises. Each recipe is designed to teach a lesson about one or more of the four elements, helping readers internalize the principles.

What sets this book apart is its emphasis on developing taste and intuition rather than strict adherence to measurements and times. Nosrat encourages readers to taste constantly, adjust seasonings, and trust their senses. She explains how different cultures use these four elements in distinct ways, from Italian cooking's emphasis on good olive oil to Southeast Asian cuisine's masterful use of acid and heat. The book has become essential reading for both home cooks and professional chefs, praised for demystifying cooking and making it accessible to everyone. By mastering these four elements, readers can cook anything, anywhere, with confidence and creativity."
            },
            new()
            {
                Id = "book-004",
                Name = "Dune",
                Description = @"Dune is a science fiction masterpiece by Frank Herbert, published in 1965. Set in the distant future amidst a sprawling feudal interstellar society, the novel tells the story of young Paul Atreides, whose family accepts stewardship of the desert planet Arrakis. This harsh world is the only source of melange, or 'spice,' the most valuable substance in the universe—a drug that extends life, enhances mental abilities, and is essential for space travel. Control of Arrakis means control of the spice, and control of the spice means control of the universe.

The novel begins as Duke Leto Atreides moves his family from their oceanic homeworld of Caladan to Arrakis, also known as Dune, taking over spice mining operations from their bitter enemies, House Harkonnen. The Duke knows this is a trap orchestrated by the Emperor and the Harkonnens, but honor compels him to accept. Paul, the Duke's son, has been trained by his mother Jessica, a member of the mystical Bene Gesserit sisterhood, in their mental and physical disciplines. He begins having prophetic visions that suggest he may be the Kwisatz Haderach, a messianic figure long prophesied by the Bene Gesserit.

When the Harkonnens attack with the Emperor's help, betraying the Atreides and killing the Duke, Paul and Jessica flee into the desert. There they encounter the Fremen, the native people of Arrakis who have adapted to survive in the planet's brutal environment. The Fremen have their own prophecies about a messiah who will lead them to freedom, and they come to believe Paul is that savior. Paul learns the ways of the Fremen, discovers the secrets of the giant sandworms, and becomes a formidable leader. He takes the name Muad'Dib and leads the Fremen in a holy war that will reshape the galaxy.

Herbert's novel is far more than a simple adventure story. It explores complex themes including politics, religion, ecology, technology, and human evolution. The book presents a sophisticated analysis of power—how it's acquired, maintained, and corrupted. It examines religious manipulation and the dangers of messianic leaders, even as Paul himself becomes trapped by the very prophecies he tries to navigate. The detailed ecology of Arrakis, with its interconnected systems of sandworms, spice, and water scarcity, reflects Herbert's deep interest in environmental science. Dune has influenced countless works of science fiction and remains a profound meditation on destiny, free will, and humanity's future."
            },
            new()
            {
                Id = "book-005",
                Name = "The Girl with the Dragon Tattoo",
                Description = @"The Girl with the Dragon Tattoo is a psychological thriller by Swedish author Stieg Larsson, the first book in the Millennium series. The novel weaves together two parallel narratives: a decades-old disappearance mystery and an exposé of corporate corruption. Mikael Blomkvist, a journalist recently convicted of libel, is hired by wealthy industrialist Henrik Vanger to investigate the forty-year-old disappearance of his niece, Harriet Vanger, who vanished from the isolated Vanger family island during a family gathering.

As Blomkvist delves into the case, he uncovers dark secrets about the Vanger family, including connections to a series of brutal murders that occurred decades earlier. He partners with Lisbeth Salander, a brilliant but deeply troubled young hacker with a photographic memory and exceptional research skills. Lisbeth, the titular girl with the dragon tattoo, is a ward of the state with a traumatic past who lives on the margins of society. Despite her punk appearance and antisocial behavior, she possesses extraordinary investigative talents and a fierce sense of justice.

The investigation leads them through Sweden's dark history, uncovering a pattern of violence against women that connects the Vanger family's past to the present. The novel alternates between the mystery investigation and Lisbeth's own struggles with a corrupt legal system and abusive guardians. When her new guardian sexually assaults her, Lisbeth takes brutal revenge, showcasing her unwillingness to be victimized. As Blomkvist and Lisbeth work together, they form an unlikely partnership built on mutual respect and trust.

Larsson's novel is a searing indictment of misogyny and violence against women in Swedish society—the original Swedish title translates to 'Men Who Hate Women.' The book explores themes of corruption in business and government, the abuse of power, journalistic integrity, and the long shadows cast by family secrets. Lisbeth Salander has become an iconic character in crime fiction, a fierce, damaged anti-hero who refuses to conform to society's expectations. The intricate plot combines elements of family saga, detective story, corporate thriller, and social commentary. The novel's success launched a global phenomenon, spawning sequels, films in multiple languages, and countless imitators, while establishing Larsson as a master of Nordic noir."
            },
            new()
            {
                Id = "book-006",
                Name = "Pride and Prejudice",
                Description = @"Pride and Prejudice is Jane Austen's beloved romantic novel, published in 1813. Set in rural England at the turn of the 19th century, it follows the Bennet family, particularly the second eldest daughter Elizabeth, as she navigates issues of marriage, morality, education, and social standing. The novel opens with one of the most famous lines in English literature: 'It is a truth universally acknowledged, that a single man in possession of a good fortune, must be in want of a wife.' This sets the stage for a story about marriage in a society where women's financial security depends on finding a suitable husband.

The arrival of the wealthy Mr. Bingley and his even wealthier friend Mr. Darcy in the neighborhood of Longbourn disrupts the peaceful lives of the Bennet sisters. While the sweet-natured Jane quickly attracts Bingley's attention, Elizabeth finds herself constantly at odds with the proud and seemingly arrogant Mr. Darcy. Their initial encounters are marked by misunderstandings and mutual prejudice—Elizabeth sees Darcy as insufferably proud, while Darcy struggles with his growing attraction to Elizabeth despite his perception of her lower social standing and her family's impropriety.

As the story unfolds, both Elizabeth and Darcy must confront their own flaws and prejudices. Elizabeth's opinion of Darcy is further damaged by the charming Mr. Wickham, who tells her false stories about Darcy's past mistreatment of him. Meanwhile, Darcy works behind the scenes to separate Bingley from Jane, believing the Bennet family beneath his friend's station. When Darcy proposes to Elizabeth in a manner that insults her family while declaring his love, she rejects him forcefully. This rejection prompts Darcy to write a letter explaining his actions and revealing Wickham's true character.

Elizabeth's subsequent travels take her to Darcy's grand estate of Pemberley, where she sees a different side of him through the eyes of his servants and his genuine affection for his sister. When her youngest sister Lydia elopes with the villainous Wickham, threatening the family's reputation, Darcy secretly intervenes to arrange a marriage and save the Bennets from ruin. This act of generosity, done without expectation of reward, reveals the true nobility of his character. Elizabeth realizes she has misjudged him completely, and when Darcy proposes again, she accepts. Through wit, social satire, and psychological insight, Austen explores the importance of looking beyond first impressions, the value of self-awareness and personal growth, and the possibility of finding true love that transcends social prejudice."
            }
        };
        
        return books;
    }
}

