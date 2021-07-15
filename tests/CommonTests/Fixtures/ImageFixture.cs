﻿using Bogus;
using Domain.Entities;
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
                    var imageContent = client.DownloadData(f.Image.PicsumUrl());

                    return imageContent;
                }
            });

            return faker.Generate();
        }
    }
}