using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParserFlights.Infrastructure;

namespace ParserFlights.Models
{
   public class SearchRouteParameters
    {
		[DisplayName("Пункт отправления(3-х буквенный код ИАТА. Например, MOW - Москва)")]
        [Required(ErrorMessage = "Введите город отбытия")]
		[RegularExpression(@"^[A-Za-z]{3}$", ErrorMessage = "Введенный код не является 3-х буквенным кодом ИАТА")]
        public string Source { get; set; }
		
		[DisplayName("Пункт назначения(3-х буквенный код ИАТА. Например, PAR - Париж)")]
        [Required(ErrorMessage = "Введите пункт назначения")]
        [RegularExpression(@"^[A-Za-z]{3}$", ErrorMessage = "Введенный код не является 3-х буквенным кодом ИАТА")]
        public string Destination { get; set; }

		[DisplayName("Дата отправления")]
        [Required(ErrorMessage = "Введите дату отправления")]
        [RegularExpression(@"^[0-9]{6}$", ErrorMessage = "Введите дату в формате yymmdd")]
        public string DateSource { get; set; }
		
		[DisplayName("Дата отбытия")]
        [Required(ErrorMessage = "Введите дату отбытия")]
        [RegularExpression(@"^[0-9]{6}$", ErrorMessage = "Введите дату в формате yymmdd")]
        public string DateDestination { get; set; }
    }
}
