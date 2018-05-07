using Prism.Events;

namespace Maya.Prism.Events
{
    /// <summary> Publish an event indicating the activity indicator will be shown. </summary>
    public class ShowActivityIndicatorEvent : PubSubEvent<string> { }

    /// <summary> Publish an event indicating the activity indicator should be hide. </summary>
    public class HideActivityIndicatorEvent : PubSubEvent { }
}
