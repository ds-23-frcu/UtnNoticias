﻿using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace UtnNoticias.User
{
	public class UserDto : EntityDto<Guid>
	{
		public Guid Id { get; set; }
		public string Username { get; set; }
	}
}
