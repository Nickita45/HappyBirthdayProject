using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class Credits : MonoBehaviour
{
    public TextMeshProUGUI creditsText;
    public float scrollSpeed = 1.0f;

    void Start()
    {
        StartCoroutine(ScrollCredits());
    }

    IEnumerator ScrollCredits()
    {
        // Напишите свой текст титров здесь.
        string credits = "Титры\n\nСпасибо тебе большое что выдержала этот щедевр!\nНа разработку ушел весь бюджет Геншин Импакт Х Хком"
        +"\n..."
        +"\nТакже хоче поблагодарить отдельно следующих людей которые помогали с разработкой\n"
        +"\nДмитрия Ветохина за прекрасный фотошоп и помощь с сюжетом 3 ночи"
        +"\nСпасибо Борису за ночь с Выживало"
        +"\nСпасибо Илье за продумывание сюжета с Геншином и помощью с модельками"
        +"\nСпасибо Максиму за разработку последнего уровня и продумывания сюжета с Геншином!"
        +"\nБез вас ребят это было бы очень тяжело!"
        +"\n\n\nLisa Game Jam is Over\n\n"

        + "Тепер будет куча поздравлений!\n Поехали!\n\n"
        +"\nКожен рік це книга з 365 порожніх сторінок... Тож створюй кожного дня по шедевру, використовуючи всі кольори життя... і щоб у кожному дні коли ти щось пишеш на обличчі була посмішка! Нехай ти побачиш все прекрасне в цьому світі, і всі твої мрії стануть реальністю. З днем народження тебе!!!\n(c) Lyzzaryz"
        +"\n\nЛіза, вітаю тебе з днем народження, бажаю тобі всього найкращого, щоб ти була завжди щаслива, здорова, щоб тобі завжди супроводжувала вдача, також бажаю тобі мирного неба над головою і багато нових фігурок з Ренгоку\n(c) Dusty"
        +"\n\nЛіза, люблю тебе сильно, дякую за все і вітаю з днем народження, шоби шиза не стояла і гроші були🫡\n(c) HollowSamurai"
        +"\n\nОт сердца и почек дарю тебе цветочек\n(c) Владик"
        +"\n\nВітаю тебе, Лізун Плотнік-Кобила, з днем народження!\nСподіваюсь після нього, в твоїй кімнаті залишиться ще менше вільного місця на стіні, а на столі з'явиться ще більше дрібничок. Бажаю тобі, щоб ти не сумувала по всіляким дурницям, щоб ти не переймалася за свій скіл в іграх і щоб, звісно, ти знайшла своє щастя серед цього гейського світу. А, ну, і видали вже раз і на завжди овервотч, що за крінж, якого фіга він досі стоїть в тебе.\nГарного тобі дня, чи вечора, чи ночі, чи коли ти це читаєш.\nP. S. Ілля гей.\n(c) Павел"
        +"\n\nЛіза з днем народження! Бажаю тобі щоб усі твої бажання та мрії збувалися\n(c) Денис"
        +"\n\nНе бійся стрибати зі скелі, а поки летиш униз, відрощуй крила.\nЗ днем народження Ліза.\n(c) Чугуйстер"
        +"\n\nЛиза Лиза Лизочка, на завтрак нам сосисочка. Кушай ешь и будь здорова, больше счастья и любви в жизни твоей пусть будет море,\nУспехов в делах, удачи на пути,\nИ пусть сбудутся все твои мечты, это говорит Альберт от всей души\n(c) Альберт Енштейн"
        +"\n\n>----лист-для-лізоньки-----<\nз днем народження Ліза!!!\nхоча я й не часто в чаті, але все одно дуже круто що там є такі крутиші як ти!!\nживи круто, нічого не бійся й все буде!!! \n(⁠✿⁠ ⁠♡⁠‿⁠♡⁠)\n(c) Тувубумба"
        +"\n\nВітаю з днем народження! Відпочивай і насолоджуйся життям. Бажаю висипатися і щоб було усе чілово. Приємних каток в овервотчі!!\nP. S. Го в доту\n(c) Maksym Shcherbak"
        +"\n\nс др, чел, желаю скушать уже наконец то все сырки мира и не обрыгаться\n(c) Илья"
        +"\n\nС Днём Рождения!\n\nДля сердца- любви,\nДля души - вдохновения,\nДля нового дня - новых сил и везения,\nДля новой дороги - мечты настоящей,\nДля жизни - огромного светлого счастья!\n(c) Демьян"
        +"\n\nВітаю Лізооо, я бажаю тобі бути багатою як чіо, енергічною як томо та розумною як осака.\nЗ днем народження!\n(c) Hrant"
        +"\n\nЛиза с днём рождения! Больше тебе ренгок и крысавчиков! Нераков в катках и в жизни! И чтоб жизнь была такая же легкая по прохождению как и эта игра! Вела щастя!\n(c) Бориска"
        +"\n\nЛіза з дн!!! люблю і обіймаю, бажаю тобі щоб в хейдіс 2 було ачівок ще більше і головним треком був трек седсвіта\nя рада що ти в нас є, дякую за те що ти таке сонечко ☼\nбажаю поменше дедінсайдства, поменше пар, побільше радісних моментів і щоб в тебе вийшло все, що ти задумала!!\n(c) .машка"
        +"\n\nГав-гав-гав патриотічно\n(c) Пес Патрон"
        +"\n\nЗавидуйте мне! Моё поздравление после пса Патрона!!!!!!!\nЛиза, с днём рождения!🥳 🎂🎉Желаю вечных скидок в стиме в 90%, чтобы нахер отменили экзамены в унике и поставили автомат по всем предметам. И чтобы вышло продолжение всех твоих любимых анимешек.\nБоря посоветовал пожелать обезьянок🐒, поэтому не желаю обезьянок(ибо нельзя прогибаться под этими мужланами хахаха)\n(c) Дашка Какашка"
        +"\n\nКонічіва (★≧▽^))★☆\nХочу привітати тебе з дуже важливим святом, а саме з Днем твого народження! \nНехай твої дні були веселими, а ночі - спокійними і, головне, безпечними (дні також ахах). \nА взагалі... Бажаю тобі всього найкращого! Така дуже крута, творча, весела та добра особистість тільки на таке і заслуговує ❤️)))\nЩе раз вітаю, сонечко\n(c) anrior"
        +"\n\nПривет Лиза. С днем рождения тебя варения хуения вот. Рифма с сосисочкой смешная вышла.\n(c) Бетховин"
        +"\n\nДоча Пиписка, [22.09.2023 23:01]\nпоздравляю тебя с др из будущего где тебе еще не больше лет\n(c) Доча Пиписка";
        // Устанавливаем текст титров.
        creditsText.text = credits;

        while (creditsText.rectTransform.anchoredPosition.y < creditsText.preferredHeight)
            {
                creditsText.rectTransform.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;
                yield return null;
            }

            // Сброс положения текста перед следующим циклом.
            creditsText.rectTransform.anchoredPosition = new Vector2(creditsText.rectTransform.anchoredPosition.x, 0);

            // Добавьте паузу или другую логику между циклами, если необходимо.
            yield return new WaitForSeconds(2.0f);

        // Завершаем титры, например, переключением на следующую сцену.
        Debug.Log("Credits finished!");
    }
}
