using System;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;

namespace BomSample
{
    public class BomAdventures
    {
        [Fact]
        public void Utf8Strings()
        {
            var initial = "Hello world!";

            using var ms = new MemoryStream();
            using var writer = new StreamWriter(ms, Encoding.UTF8);

            writer.Write(initial);
            writer.Flush();

            Assert.Equal(
                initial,
                Encoding.UTF8.GetString(ms.ToArray()));
        }

        [Fact]
        public void Utf8Arrays()
        {
            var initial = "Hello world!";

            using var ms = new MemoryStream();
            using var writer = new StreamWriter(ms, Encoding.UTF8);

            writer.Write(initial);
            writer.Flush();

            Assert.Equal(
                Encoding.UTF8.GetBytes(initial),
                ms.ToArray());
        }

        [Fact]
        public void ItIsTheBom()
        {
            Assert.Equal(
                new[] { 0xEF, 0xBB, 0xBF },
                new[] { 239, 187, 191 });

            Assert.Equal(
                new byte[] { 239, 187, 191 },
                Encoding.UTF8.GetPreamble());
        }

        [Fact]
        public void Utf8StringsWithoutBom()
        {
            var initial = "Hello world!";

            using var ms = new MemoryStream();
            using var writer = new StreamWriter(ms, new UTF8Encoding(false));

            writer.Write(initial);
            writer.Flush();

            Assert.Equal(
                initial,
                Encoding.UTF8.GetString(ms.ToArray()));
        }

        [Fact]
        public void Outsmarted()
        {
            var initial = "Hello world!";
            var encWithBom = new UTF8Encoding(true);
            var encWithoutBom = new UTF8Encoding(false);

            var rWithBome = encWithBom.GetBytes(initial);
            var rWithoutBom = encWithoutBom.GetBytes(initial);

            Assert.NotEqual(
                rWithBome,
                rWithoutBom);
        }

        //No, it's the StreamWriter that makes use of the Preamble for the encoding
    }
}
