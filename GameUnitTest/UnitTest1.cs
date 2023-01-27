using ConsoleGame;

namespace GameUnitTest
{
    public class Tests
    {
        Map _map;
        bool _spritesLoaded = false;

        [SetUp]
        public void Setup()
        {
            if (!_spritesLoaded)
            {
                Sprite.LoadSprites();
                _spritesLoaded = true;
            }

            _map = new Map();
            _map.Load();
        }

        [Test]
        public void EmptyMap()
        {
            Map map = new Map();
            Assert.That(map.GetSprites(), Is.Empty);
        }

        [Test]
        public void IsMapValidAfterLoad()
        {
            Assert.That(_map.GetSprites(), Is.Not.Empty);
            Assert.That(_map.GetImageMap(), Is.Not.Empty);
        }

        [Test]
        [TestCase('█', true)]
        [TestCase('#', false)]
        [TestCase(' ', false)]
        public void IsCollidable(char tileType, bool expected)
        {
            Assert.That(Map.IsCollidable(tileType), Is.EqualTo(expected));
        }

        [Test]
        [TestCase(20, 5, false)]
        [TestCase(26, 28, true)]
        [TestCase(15, 40, false)]
        public void IsCollidableByCoords(int x, int y, bool expected)
        {
            Assert.That(_map.IsCollidable(x, y), Is.EqualTo(expected));
        }
    }
}
