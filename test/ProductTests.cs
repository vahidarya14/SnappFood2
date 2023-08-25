using Core.Domain;
using Humanizer;

namespace test
{
    public class ProductTests
    {

        [Test]
        public void creation_product_ok()
        {
            Assert.That( "asmbasdvasvasgvas", Is.EqualTo(new Product("asmbasdvasvasgvas").Title));
        }


        [Test]
        public void throw_exception_when_title_begger_than_40_length()
        {
            var ex = Assert.Throws<Exception>(() => {
                var email = new Product("asmbasdvasvasgvasasmbasdvasvasgvasasmbasdvasvasgvasasmbasdvasvasgvasasmbasdvasvasgvas");
            });
            Assert.That(ex.Message, Is.EqualTo("title must be less than 40"));
        }
    }
}