using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;
using static System.Net.Mime.MediaTypeNames;

namespace SpecFlowProject2
{
    class LoginOneTime
    {
        private IBrowser _browser;
        private IBrowserContext _context;
        private IPage _page;

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            var playwright = await Playwright.CreateAsync();
            _browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false,
                Channel = "chrome",
                SlowMo = 2000
            });

            _context = await _browser.NewContextAsync();
            _page = await _context.NewPageAsync();

            await Login();
        }

        [OneTimeTearDown]
        public async Task OneTimeTearDown()
        {
            await _context.CloseAsync();
            await _browser.CloseAsync();
        }

        private async Task Login()
        {
            await _page.GotoAsync("http://www.eaapp.somee.com");
            await _page.Locator("//a[text()='Employee List']").ClickAsync();
            var lnkLogin = _page.Locator("//a[@id='loginLink']");
            await lnkLogin.ClickAsync();

            await _page.FillAsync("//input[@id='UserName']", "admin");
            await _page.FillAsync("//input[@id='Password']", "password");
            await _page.ClickAsync("text= log in");


            await _page.CloseAsync();
        }


        [Test]
        public async Task Test1()
        {
            // Tạo một trang mới từ context đã có storage state
            var newPage = await _context.NewPageAsync();

            // Mở trang mới và kiểm tra trạng thái đăng nhập
            await newPage.GotoAsync("http://www.eaapp.somee.com");

            await newPage.Locator("//a[text()='Employee List']").ClickAsync();
            // Đóng trang mới
            await newPage.CloseAsync();
        }


        [Test]
        public async Task Test2()
        {
            // Tạo một trang mới từ context đã có storage state
            var newPage = await _context.NewPageAsync();

            // Mở trang mới và kiểm tra trạng thái đăng nhập
            await newPage.GotoAsync("http://www.eaapp.somee.com");
            await newPage.Locator("//a[text()='Employee List']").ClickAsync();

            // Đóng trang mới
            await newPage.CloseAsync();
        }

    }
}