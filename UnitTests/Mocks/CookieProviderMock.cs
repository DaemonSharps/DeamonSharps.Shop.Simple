using DeamonSharps.Shop.Simple.Services.Interfaces;
using Moq;

namespace DaemonSharps.Shop.UnitTests.Mocks
{
    public class CookieProviderMock : Mock<ICookieProvider>
    {
        private readonly int _catId;
        public CookieProviderMock(int categoryId)
        {
            _catId = categoryId;
            Set();
        }

        private void Set()
        {
            Setup(cp => cp.GetCookieValue(It.Is<string>(s => s == "categoryId")))
                .Returns($"{_catId}");
        }
    }
}
