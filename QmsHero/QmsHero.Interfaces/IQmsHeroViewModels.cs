using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QmsHero.Interfaces
{
    public interface IQmsHeroViewModels
    {
        IPageViewModel MainViewModel { get; set; }
        IPageViewModel CustomProcessingViewModel { get; set; }

    }
}
