const puppeteer = require('puppeteer');

async function getCheckScreen() {
    const browser = await puppeteer.launch({
        headless: false,
        args: ['--start-fullscreen']
    });
    const page = await browser.newPage();

    let task = {
        name: 'тест'
    }

    let numbCourse = '1'

    let user = {
        login: 'test123',
        password: 'test123'
    }

    let planClass = {
        name: 'test',
        poll: 'test',
        block: 'test'
    }

    const timer = 1000
    try {
        await page.goto('https://localhost:44326/')
        // await page.goto('http://www.teachersoffice.somee.com/');
        await page.waitFor(timer);
        await page.setViewport({
            width: 1366,
            height: 768
        });

        //вход  в кабинет
        await page.focus('#inputTeacherLogin')
        page.keyboard.type(user.login)
        await page.waitFor(timer);
        await page.focus('#inputTeacherPassword')
        page.keyboard.type(user.password)
        await page.screenshot({
            path: './screens result/Заполненное окно входа.png'
        });
        await page.waitFor(timer);
        await page.click('#nav-teacher > form > div.d-flex.justify-content-end > input')
        await page.waitFor(timer);

        await page.click(`body > div > main > div.container-fluid.mt-3 > div.row > div:nth-child(${numbCourse}) > div > div > form:nth-child(2) > div.d-flex.bd-highlight > a`)
        await page.waitFor(timer);
        await page.screenshot({
            path: './screens result/Подробнее.png'
        });

        await page.click(`body > div > main > div.container.mt-5 > div > div.modal-body > div.d-flex.justify-content-between > a:nth-child(1)`)
        await page.waitFor(timer);

        await page.click(`body > div > main > table > tbody > tr > td:nth-child(7) > form > a:nth-child(1)`)
        await page.waitFor(timer);

        await page.screenshot({
            path: './screens result/Занятие.png'
        });

        await page.click(`body > div > main > div:nth-child(4) > div > div > form.d-flex.bd-highlight.mb-1 > input.btn.btn-outline-danger.ml-auto.p-1.bd-highlight`)
        await page.waitFor(timer);

        await page.screenshot({
            path: './screens result/План занятия.png'
        });
        await page.waitFor(timer);


        await browser.close();


    } catch (error) {
        console.log(error)
    }
}

getCheckScreen()