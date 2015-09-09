namespace System.Collections.Generic.ThreadSafe
{
	/// <summary>
	/// Eine .NET-Queue, die für den Multithread-Einsatz gedacht ist.
	/// Unbegrenzter Platz in der Queue durch Monitor anstelle von Semaphore.
	/// Typsicherheit durch einen generischen Typ-Parameter.
	/// Threadsicherheit durch die lock()-Funktionalität.
	/// Enqueue() blockiert nie.
	/// Dequeue() blockiert nie, sondern gibt null zurück, wenn kein Element
	/// vorhanden ist.
	/// </summary>
	/// <ItemTypeparam name="ItemType">Der zu speichernde Typ in der Queue. Der Typ muss
	/// ein Referenztyp sein, damit null ein gültiger Rückgabewert sein kann.
	/// </ItemTypeparam>
	public class UnboundedNonBlockingQueue<ItemType> where ItemType : class
	{
		private readonly System.Object _Lock;
		private readonly System.Collections.Generic.Queue<ItemType> _Queue;
		
		/// <summary>
		/// Erzeugt eine neue leere Queue.
		/// </summary>
		public UnboundedNonBlockingQueue()
		{
			_Lock = new System.Object();
			_Queue = new System.Collections.Generic.Queue<ItemType>();
		}
		
		/// <summary>
		/// Entfernt das erste Element der Queue und gibt es zurück.
		/// Die Funktion gibt null zurück, wenn kein Element verfügbar ist.
		/// </summary>
		/// <returns>Das erste Element der Queue oder null, wenn kein Element verfügbar ist.</returns>
		public ItemType Dequeue()
		{
			ItemType Result = null;
			
			lock(_Lock)
			{
				if(_Queue.Count > 0)
				{
					Result = _Queue.Dequeue();
				}
			}
			
			return Result;
		}
		
		/// <summary>
		/// Fügt der Queue eine weiteres Element hinten an.
		/// Kann durch konkurierenden Zugriff kurzzeitig blockieren - längere Blockaden sind ausgeschlossen.
		/// Es gibt keinen maximalen Füllstand für die Queue.
		/// </summary>
		/// <param name="Value">Das anzuhängende Element.</param>
		public void Enqueue(ItemType Value)
		{
			lock(_Lock)
			{
				_Queue.Enqueue(Value);
			}
		}
		
		public void Clear()
		{
			lock(_Lock)
			{
				while(_Queue.Count > 0)
				{
					_Queue.Dequeue();
				}
			}
		}
	}
}
