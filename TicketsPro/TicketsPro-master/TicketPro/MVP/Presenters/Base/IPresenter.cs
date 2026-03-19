using System;
namespace TicketPro.MVP.Presenters.Base
{
    public interface IPresenter<TView> where TView : Views.Base.IView
    {
        TView View { get; }
        void OnViewLoad();
    }
}
