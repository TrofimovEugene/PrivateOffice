const puppeteer = require('puppeteer');

async function getCheckScreen() {
  const browser = await puppeteer.launch({
    headless: false
  });
  const page = await browser.newPage();

  let lesson = {
    topic: 'Тест',
    type: '2',
    day: 'Вторник',
    date: '12032020',
    cabinet: 'D505',
    starttime: '13:50',
    endtime: '15:20',
    replay: 'каждую неделю'
  }

  let user = {
    login: 'test123',
    password: 'test123'
  }

  let course = '1'

  const timer = 1000
  try {
    await page.goto('http://www.teachersoffice.somee.com/');
    // await page.goto('https://localhost:44326/');
    await page.waitFor(timer);
    await page.setViewport({
      width: 1000,
      height: 700
    })

    //вход  в кабинет
    await page.focus('#inputTeacherLogin')
    page.keyboard.type(user.login)
    await page.waitFor(timer);
    await page.focus('#inputTeacherPassword')
    page.keyboard.type(user.password)
    await page.waitFor(timer);
    // await page.screenshot({path: './screens result/Заполненное окно входа.png'});
    // await page.waitFor(timer);
    await page.click('#nav-teacher > form > div.d-flex.justify-content-end > input')
    await page.waitFor(timer);

    //подробнее
    await page.click(`body > div > main > div.container-fluid.mt-3 > div.row > div:nth-child(${course}) > div > div > form:nth-child(2) > div.d-flex.bd-highlight > a`)
    await page.waitFor(timer);
    await page.screenshot({
      path: './screens result/Подробнее.png'
    });

    //добавление занятия
    await page.click('body > div > main > div.container.mt-5 > div > div.modal-body > div.d-flex.justify-content-between > a:nth-child(1)')
    await page.waitFor(timer);

    await page.screenshot({
      path: './screens result/Таблица занятий.png'
    });
    await page.waitFor(timer);

    await page.click('body > div > main > div.container-fluid.mt-3 > div > button')
    await page.waitFor(timer);

    await page.click('#addNewClass > div > div > div.modal-body.pb-0 > div:nth-child(1)');
    page.keyboard.type(lesson.topic);
    await page.waitFor(timer);

    await page.select('select[id="TypeClass"]', lesson.type);
    await page.waitFor(timer);

    await page.focus('#InputDay')
    await page.select('select[name="Class.DaysWeek"]', lesson.day);
    await page.waitFor(timer);

    await page.focus('input[name="Class.DateClasses"]')
    page.keyboard.type(lesson.date)
    await page.waitFor(timer);

    await page.focus('input[name="Class.Cabinet"]')
    page.keyboard.type(lesson.cabinet)
    await page.waitFor(timer);

    await page.focus('input[name="Class.StartTime"]')
    page.keyboard.type(lesson.starttime)
    await page.waitFor(timer);

    await page.focus('input[name="Class.EndTime"]')
    page.keyboard.type(lesson.endtime)
    await page.waitFor(timer);

    await page.focus('input[name="Class.ReplayClasses"]')
    page.keyboard.type(lesson.replay)
    await page.waitFor(timer);

    // await page.screenshot({path: './screens result/Добавление занятия.png'});
    // await page.waitFor(timer);

    await page.click('#addNewClass > div > div > div.modal-footer > input')
    await page.waitFor(timer);

    await browser.close();
  } catch (error) {
    console.log(error)
  }
}

getCheckScreen()