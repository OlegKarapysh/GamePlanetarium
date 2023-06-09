using GamePlanetarium.Domain.GameSeeds;

namespace GamePlanetarium.Domain.Game;

public class QuestionsSeedEng : QuestionsSeed
{
    public override QuestionTextData[] QuestionsText { get; } =
    {
        new QuestionTextData(
            "Is it possible to see your zodiac constellation in the starry sky on your birthday?", new[]
            {
                "Yes!",
                "No, because on your birthday, the Sun is situated in this constellation, " +
                "and the sunlight is the obstacle to seeing it",
                "No matter! Twelve zodiac constellations appear in the sky in 24 hours"
            }),
        new QuestionTextData(
            "Why do we see the Moon in different colors?", new[]
            {
                "The Moon is lit by different lamps",
                "The color depends on the temperature of the Moon’s surface",
                "The Earth’s atmosphere is an optical filter scattering sunlight, " +
                "so the color of the Moon depends on how the Sun’s rays pass through it.",
            }),
        new QuestionTextData(
            "Watching the starry sky, we look into the past. How deep can modern telescopes look into the past?", new[]
            {
                "At the time of the formation of the first stars (13.5 billion years ago)",
                "At the time of the formation of the Milky Way Galaxy (13 billion years ago)",
                "At the time of the formation of the Solar System (4.6 billion years ago)"
            }),
        new QuestionTextData(
            "Why does Mercury have a higher orbital velocity than Neptune?", new[]
            {
                "Mercury is more active than Neptune",
                "Neptune is more massive than Mercury, so it is very difficult for it to move",
                "Because of the Sun’s gravity"
            }),
        new QuestionTextData(
            "Where is the Solar System located in the Milky Way Galaxy?", new[]
            {
                "In the center",
                "On the outskirts",
                "In the Orion Arm"
            }),
        new QuestionTextData(
            "Which artificial satellite in Earth’s orbit is the biggest?", new[]
            {
                "The International Space Station",
                "Ocean-1",
                "Tesla"
            }),
        new QuestionTextData(
            "Which launch vehicle did deliver the first dog Laika into space?", new[]
            {
                "A not very reliable one",
                "Sputnik 2",
                "Space Shuttle Columbia"
            }),
        new QuestionTextData(
            "Which of these is not the name of a nebula?", new[]
            {
                "The Rotten Egg",
                "The Ghost of Saturn",
                "The Philosopher’s Stone"
            }),
        new QuestionTextData(
            "Do black holes exist forever?", new[]
            {
                "Yes! Because they are constantly swallowing up everything around them",
                "Black holes don’t exist. It is a conspiracy theory",
                "After the discovery of Hawking radiation, scientists believe that black holes are evaporating"
            }),
        new QuestionTextData(
            "How did scientists find out the volume of the Sun, the mass of black holes, " +
            "and the number of stars in the galaxy?", new[]
            {
                "It is a result of the joint efforts of astronomers, mathematicians, and physicists",
                "There have been successful space expeditions studying every object in the Universe",
                "Scientists just imagined things to avoid being asked silly questions"
            }),
    };
}