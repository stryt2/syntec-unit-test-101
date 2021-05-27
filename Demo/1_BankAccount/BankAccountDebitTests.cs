using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

namespace Demo._1_BankAccount
{
	/// <summary>
	/// verify the behavior of the 'Debit' method of the BankAccount class.
	/// </summary>
	class BankAccountDebitTests
	{
		[Test]
		public void Debit_WithValidAmount_UpdatesBalance()
		{
			// TODO: Verify that a valid amount (that is, one that is less than the account
			// balance and greater than zero) withdraws the correct amount from the account.

			// Given beginning balance 11.99
			// When debit with amount 4.55
			// Then balance should be 7.44 with tolerance 0.001
			// And should give reasonable error message like "Account not debited correctly" if failed.
		}

		// TODO: Add more tests to cover other behaviors:
		// 1. less than zero
		// 2. more than balance
	}
}
