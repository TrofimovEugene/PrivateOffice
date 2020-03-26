const puppeteer = require('puppeteer');

async function getCheckScreen() {
    const browser = await puppeteer.launch({
        headless: false,
        args: ['--start-fullscreen']
    });
    const page = await browser.newPage();

    let task = {
        name: 'тест',
        class: '5',
        typeTask: 'тест'
    }

    let numbStudent = '1'

    let user = {
        login: 'test123',
        password: 'test123'
      }

    let numberGroup = '3'

    const timer = 1000
    try {
        await page.goto('http://www.teachersoffice.somee.com/');
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

        //добавление группы
        await page.click('body > div > main > div.container-fluid.mt-3 > div.d-flex.align-items-start.flex-column.bd-highlight.mb-3 > a')
        await page.waitFor(timer);
        await page.screenshot({
            path: './screens result/Таблица групп.png'
        });
        await page.waitFor(timer);
        await page.click(`body > div > main > form > table > tbody > tr:nth-child(3) > td:nth-child(3) > form > a:nth-child(1)`)
        await page.waitFor(timer);
        await page.click(`#tab1 > tbody > tr:nth-child(${numbStudent}) > td:nth-child(4) > form > a:nth-child(2)`)
        await page.waitFor(timer);
        await page.click('body > div > main > div.container-fluid.mt-3 > div > button')
        await page.waitFor(timer);
        await page.focus('input[name="Homework.ContentHomework"]')
        page.keyboard.type(task.name)
        await page.select('select[name="Idclasses"]', task.class);
        await page.waitFor(timer);
        await page.focus('input[name="Homework.TypeHHomework"]')
        page.keyboard.type(task.typeTask)
        await page.waitFor(timer);
        await page.screenshot({
            path: './screens result/Добавление задания.png'
        });
        await page.click('#addStudent > div > div > div.modal-footer > input')
        await page.waitFor(timer);
        await page.screenshot({
            path: './screens result/Задания.png'
        });
        await page.waitFor(timer);
        await browser.close();


    } catch (error) {
        console.log(error)
    }
}

getCheckScreen()