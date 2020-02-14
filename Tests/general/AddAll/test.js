const puppeteer = require('puppeteer');

async function getCheckScreen() {
  const browser = await puppeteer.launch({headless: false});
  const page = await browser.newPage();

  let lesson = {
      topic: 'Тест',
      type:'2',
      day:'Вторник',
      timeStart:'12',
      timeEnd:'14',
      period:'каждую неделю'
  }

  let user = {
    login:'test123',
    password:'test123' 
  }

  let course = {
      universiry: 'ИрГУПС',
      group: '3',
      nameCourse: 'Тест',
      startCourse:'12 12 2019',
      endCourse: '12 02 2020',
      time: '140'
  }

  let student ={
      name:'testname',
      surname:'testsurname'
  }

  let group = {
    name: 'БИ-1-16'
  }

  const timer = 1000
  
  try {

  await page.goto('https://localhost:44326/');
  await page.waitFor(timer);
  await page.setViewport({width: 1000, height: 700})


  //вход  в кабинет
  await page.focus('#inputTeacherLogin')
  page.keyboard.type(user.login)
  await page.waitFor(timer);
  await page.focus('#inputTeacherPassword')
  page.keyboard.type(user.password)
  await page.screenshot({path: './screens result/Заполненное окно входа.png'});
  await page.waitFor(timer);
  await page.click('#nav-teacher > form > div.d-flex.justify-content-between > input.btn.btn-primary')
  await page.waitFor(timer);

  //добавление группы
  await page.click('body > div:nth-child(1) > main > div.container-fluid.mt-3 > div.form-group > a')
  await page.waitFor(timer);
  await page.click('body > div > main > button')
  await page.waitFor(timer);
  await page.focus('#NameGroup')
  page.keyboard.type(group.name)
  await page.screenshot({path: './screens result/Добавление студента.png'});
  await page.waitFor(timer);
  await page.click('#addGroups > div > div > div.modal-footer > input')
  await page.waitFor(timer);
  await page.goBack()
  await page.goBack()
  await page.waitFor(timer);

  //создание курса
  await page.click('body > div:nth-child(1) > main > div.container-fluid.mt-3 > div.row > div > button.btn-outline-primary')
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
  await page.screenshot({path: './screens result/Форма добавления курса.png'});
  await page.click('#exampleModalCenter > div > form > div > div.modal-footer > input.btn.btn-primary')
  await page.waitFor(timer);
  await page.screenshot({path: './screens result/Страница курсов.png'});
  await page.waitFor(timer);

  //подробнее
  await page.click('body > div > main > div.container-fluid.mt-3 > div.row > div:nth-child(1) > div > div > form:nth-child(1) > a')
  await page.waitFor(timer);
  await page.screenshot({path: './screens result/Подробнее.png'});

  //добавление студента
  await page.waitFor(timer);
  await page.click('body > div > main > div.container.mt-5 > div > div.modal-body > div.d-flex.justify-content-between > a:nth-child(3)')
  await page.waitFor(timer);

  await page.click('body > div:nth-child(1) > main > button')
  await page.waitFor(timer);
  
  await page.focus('#NameStudent')
  page.keyboard.type(student.name)
  await page.waitFor(timer);
  await page.focus('#SurnameStudent')
  page.keyboard.type(student.surname)
  await page.waitFor(timer);
  await page.click('#addStudent > div > div > div.modal-footer >  input.btn.btn-primary')
  await page.waitFor(timer);
  await page.screenshot({path: './screens result/Студенты.png'});

  await page.goBack()
  await page.goBack()
  await page.waitFor(timer);

  //добавление занятия
  await page.click('body > div > main > div.container.mt-5 > div > div.modal-body > div.d-flex.justify-content-between > a:nth-child(1)')
  await page.waitFor(timer);

  await page.click('body > div > main > div.mt-3.ml-1.mb-1 > button')
  await page.waitFor(timer);

  await page.click('#addNewClass > div > div > div.modal-body.pb-0 > div:nth-child(1)');
  page.keyboard.type(lesson.topic);
  await page.waitFor(timer);

  await page.select('select[id="TypeClass"]', lesson.type);
  await page.waitFor(timer);

  await page.focus('#InputDay')
  await page.select('select[id="InputDay"]', lesson.day);
  await page.waitFor(timer);

  await page.focus('#InputTimeBegin')
  page.keyboard.type(lesson.timeStart)
  await page.waitFor(timer);

  await page.focus('#InputTimeEnd')
  page.keyboard.type(lesson.timeEnd)
  await page.waitFor(timer);

  await page.focus('#InputСountClass')
  page.keyboard.type(lesson.period)
  await page.screenshot({path: './screens result/Добавление занятия.png'});
  await page.waitFor(timer);

  await page.click('#addNewClass > div > div > div.modal-footer > input.btn.btn-primary')
  await page.waitFor(timer);

  await browser.close();
  console.log('Тест успешно пройден')

  } catch (error) { 
      console.log(error)
  }
  
}

getCheckScreen();