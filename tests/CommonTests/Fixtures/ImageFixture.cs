using Bogus;
using Domain.Entities;
using Domain.ViewModels.Image;
using System.Net;
using Xunit;

namespace CommonTests.Fixtures
{
    [CollectionDefinition(nameof(ImageFixtureCollection))]
    public class ImageFixtureCollection : ICollectionFixture<ImageFixture>
    { }

    public class ImageFixture
    {
        public Image ValidImage()
        {
            var faker = new Faker<Image>("pt_BR");

            faker.RuleFor(i => i.Name, (f, i) => f.System.FileName("png"));
            faker.RuleFor(i => i.Content, (f, i) =>
            {
                using (WebClient client = new WebClient())
                {
                    var imageContent = client.DownloadData(f.Image.PicsumUrl(imageId: 1));

                    return imageContent;
                }
            });

            return faker.Generate();
        }

        public ImageViewModel ValidImageViewModel()
        {
            var faker = new Faker<ImageViewModel>("pt_BR");

            faker.RuleFor(i => i.Name, (f, i) => f.System.FileName("png"));
            faker.RuleFor(i => i.Content, (f, i) =>
            {
                using (WebClient client = new WebClient())
                {
                    var imageContent = client.DownloadString(f.Image.PicsumUrl(imageId: 1));

                    return imageContent;
                }
            });

            return faker.Generate();
        }

        public ImageOutputViewModel ValidImageOutputViewModel()
        {
            var faker = new Faker<ImageOutputViewModel>("pt_BR");

            faker.RuleFor(i => i.Name, (f, i) => f.System.FileName("png"));
            faker.RuleFor(i => i.Content, (f, i) =>
            {
                using (WebClient client = new WebClient())
                {
                    var imageContent = client.DownloadString(f.Image.PicsumUrl(imageId: 1));

                    return imageContent;
                }
            });

            return faker.Generate();
        }
    }
}
