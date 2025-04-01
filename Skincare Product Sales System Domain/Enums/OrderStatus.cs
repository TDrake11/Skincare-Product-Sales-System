using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skincare_Product_Sales_System_Domain.Enums
{
	public enum OrderStatus
	{
		Cart, // Order is in the cart
		Pending, //  Order placed, waiting for confirmation
		Confirmed, // Order has been confirmed by the system
		Processing, //  Order is being prepared/packed.
		Completed, // Order has been successfully received.
		Cancelled // Order was canceled or failed
	}
}
