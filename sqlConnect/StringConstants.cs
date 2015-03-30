namespace AutoCADTest
{
    class StringConstants
    {
        public const string CANT_FIND_LAYERS_CONFIG_FILE = "Невозможно загрузить конфигурационный файл с перечнем слоев для анализа {0}. Найдите это файл на диске и поместите в папку {1}. Или создайте новый файл в соответсвии с руководством по эксплуатации";
        public const string STAGE_WASNT_CREATED = "Стенд или мероприятие не было создано. Сперва запустите команду createstage или createevent. Или сделайте это из меню CRMExport";
        public const string GUID_PREFIX = "guid";
        public const string SERVICE_NAME_PREFIX = "s";
        public const string ACAD_FILENAME_PREFIX = "expoforum";
        public const string SEND_STAGE_TO_CRM = "Отправляем стенд в базу данных";
        public const string CREATE_STAGE = "После добавления всех элементов на чертеж, обозначьте площадь стенда в слое {0} при помощи полилинии, выделите прямоугольником элементы стенда, подлежащие импорту в CRM в слое {1}, а затем выполните команду sendcrm";
        public const string CREATE_EVENT = "Создано мероприятие {0}. Для включения внешних элементов в отдельные стенды, введите в текст гиперссылки элемента код приписываемого стенда. Код начинается с префикса {1} и может быть найден в файле соответсвующего стенда в папке {2}";
        public const string CHOOSE_OBJECTS_TO_REMOVE = "Выберите объекты для удаления: ";
        public const string CHOOSE_OBJECTS_TO_INCLUDE = "Выберите объекты для добавления: ";
    }
}
