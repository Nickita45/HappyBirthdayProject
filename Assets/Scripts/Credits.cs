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
        +"\n\n\nLisa Game Jam is Over"

        +"\nКожен рік це книга з 365 порожніх сторінок... Тож створюй кожного дня по шедевру, використовуючи всі кольори життя... і щоб у кожному дні коли ти щось пишеш на обличчі була посмішка! Нехай ти побачиш все прекрасне в цьому світі, і всі твої мрії стануть реальністю. З днем народження тебе!!!";
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
