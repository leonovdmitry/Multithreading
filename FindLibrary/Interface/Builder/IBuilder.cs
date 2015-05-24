using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindLibrary.Builder
{
	public interface IBuilder<T>
	{
		T Build();
	}
}
