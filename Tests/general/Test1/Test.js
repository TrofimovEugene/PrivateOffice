const puppeteer = require('puppeteer');

async function getCheckScreen() {
  const browser = await puppeteer.launch({
    headless: false,
    args: ['--start-fullscreen']
  });
  const page = await browser.newPage();

  let user = {
    login: 'test123',
    password: 'test123'
  }

  let numbCourse = '1'

  let student = {
    name: 'testname',
    surname: 'testsurname',
    group: '1',
    login: 'teststudent',
    password: 'teststudent'
  }

  // let numberGroup = '3'

  let course = {
    universiry: 'ИрГУПС',
    group: '1',
    nameCourse: 'Тест',
    startCourse: '12 12 2019',
    endCourse: '12 04 2020',
    time: '140'
  }

  let group = {
    name: 'Тест'
  }

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

  const timer = 1000
  try {
    await page.goto('https://localhost:44326/');
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
    await page.click('body > div > main > div.container-fluid.mt-3 > div > button')
    await page.waitFor(timer);
    await page.click('#addGroups > div > div > div.modal-footer > input')
    await page.waitFor(timer);
    await page.focus('#NameGroup')
    page.keyboard.type(group.name)
    await page.screenshot({
      path: './screens result/Добавление группы.png'
    });
    await page.waitFor(timer);
    await page.click('#addGroups > div > div > div.modal-footer > input')
    await page.waitFor(timer);
    await page.screenshot({
      path: './screens result/Новая группа.png'
    });
    await page.waitFor(timer);
    await page.click('body > div > main > div.container-fluid.mt-3 > div > a')
    await page.waitFor(timer);

    //создание курса
    await page.screenshot({
      path: './screens result/Курсы.png'
    });
    await page.waitFor(timer);
    await page.click('body > div > main > div.container-fluid.mt-3 > div.row > div > button')
    await page.waitFor(timer);
    await page.click('#exampleModalCenter > div > form > div > div.modal-footer > input.btn.btn-primary')
    await page.waitFor(timer);
    await page.focus('#InputUniveristy')
    page.keyboard.type(course.universiry)
    await page.waitFor(timer);
    await page.focus('#InputNameCourse')
    page.keyboard.type(course.nameCourse)
    await page.waitFor(timer);
    await page.select('select[name="idgroup"]', course.group);
    await page.waitFor(timer);
    await page.focus('#InputStartCourse')
    page.keyboard.type(course.startCourse)
    await page.waitFor(timer);
    await page.focus('#InputEndCourse')
    page.keyboard.type(course.endCourse)
    await page.waitFor(timer);
    await page.focus('#InputTime')
    page.keyboard.type(course.time)
    await page.screenshot({
      path: './screens result/Форма добавления курса.png'
    });
    await page.click('#exampleModalCenter > div > form > div > div.modal-footer > input.btn.btn-primary')
    await page.waitFor(timer);
    await page.screenshot({
      path: './screens result/Страница курсов.png'
    });
    await page.waitFor(timer);

    await page.click(`body > div > main > div.container-fluid.mt-3 > div.row > div:nth-child(${numbCourse}) > div > div > form:nth-child(2) > div.d-flex.bd-highlight > a`)
    await page.waitFor(timer);
    await page.screenshot({
      path: './screens result/Подробнее.png'
    });

    //добавление студента
    await page.waitFor(timer);
    await page.click('body > div > main > div.container.mt-5 > div > div.modal-body > div.d-flex.justify-content-between > a:nth-child(3)')
    await page.waitFor(timer);
    await page.screenshot({
      path: './screens result/Таблица студентов.png'
    });
    await page.waitFor(timer);

    await page.click('body > div > main > div.container-fluid.mt-3 > div > button')
    await page.waitFor(timer);
    //   await page.click('#addStudent > div > div > div.modal-footer >  input.btn.btn-primary')
    // await page.waitFor(timer);

    await page.focus('#NameStudent')
    page.keyboard.type(student.name)
    await page.waitFor(timer);
    await page.focus('#SurnameStudent')
    page.keyboard.type(student.surname)
    await page.waitFor(timer);
    await page.select('select[name="idgroup"]', student.group);
    await page.waitFor(timer);
    await page.focus('input[name="Student.Login"]')
    page.keyboard.type(student.login)
    await page.waitFor(timer);
    await page.focus('input[name="Student.Password"]')
    page.keyboard.type(student.password)
    await page.waitFor(timer);
    await page.click('#addStudent > div > div > div.modal-footer >  input.btn.btn-primary')
    await page.waitFor(timer);
    await page.screenshot({
      path: './screens result/Студенты.png'
    });
    await page.waitFor(timer);
    await page.click('body > div > main > div.container-fluid.mt-3 > div > a')
    await page.waitFor(timer);
    await page.click(`body > div > main > div.container-fluid.mt-3 > div.row > div:nth-child(${numbCourse}) > div > div > form:nth-child(2) > div.d-flex.bd-highlight > a`)
    await page.waitFor(timer);
    await page.click('body > div > main > div.container.mt-5 > div > div.modal-body > div.d-flex.justify-content-between > a:nth-child(1)')
    await page.waitFor(timer);

    // await page.screenshot({path: './screens result/Таблица занятий.png'});
    // await page.waitFor(timer);

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
    await page.click('#addNewClass > div > div > div.modal-footer > input')
    await page.waitFor(timer);

    await browser.close();


  } catch (error) {
    console.log(error)
  }
}

getCheckScreen()