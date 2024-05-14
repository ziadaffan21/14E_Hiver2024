using CineQuebec.Windows.DAL.Data;
using CineQuebec.Windows.DAL.Entities;
using Prism.Events;

namespace CineQuebec.Windows.ViewModel
{
    internal class NoterFilmEvent: PubSubEvent<Note>
    {
    }
}