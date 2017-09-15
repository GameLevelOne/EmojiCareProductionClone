#pragma warning disable 612,618
#pragma warning disable 0114
#pragma warning disable 0108

using System;
using System.Collections.Generic;
using GameSparks.Core;
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;

//THIS FILE IS AUTO GENERATED, DO NOT MODIFY!!
//THIS FILE IS AUTO GENERATED, DO NOT MODIFY!!
//THIS FILE IS AUTO GENERATED, DO NOT MODIFY!!

namespace GameSparks.Api.Requests{
		public class LogEventRequest_GET_EMOJI : GSTypedRequest<LogEventRequest_GET_EMOJI, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_GET_EMOJI() : base("LogEventRequest"){
			request.AddString("eventKey", "GET_EMOJI");
		}
	}
	
	public class LogChallengeEventRequest_GET_EMOJI : GSTypedRequest<LogChallengeEventRequest_GET_EMOJI, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_GET_EMOJI() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "GET_EMOJI");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_GET_EMOJI SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
	}
	
	public class LogEventRequest_LOAD : GSTypedRequest<LogEventRequest_LOAD, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_LOAD() : base("LogEventRequest"){
			request.AddString("eventKey", "LOAD");
		}
	}
	
	public class LogChallengeEventRequest_LOAD : GSTypedRequest<LogChallengeEventRequest_LOAD, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_LOAD() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "LOAD");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_LOAD SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
	}
	
	public class LogEventRequest_SAVE : GSTypedRequest<LogEventRequest_SAVE, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_SAVE() : base("LogEventRequest"){
			request.AddString("eventKey", "SAVE");
		}
		public LogEventRequest_SAVE Set_coin( long value )
		{
			request.AddNumber("coin", value);
			return this;
		}			
		public LogEventRequest_SAVE Set_gem( long value )
		{
			request.AddNumber("gem", value);
			return this;
		}			
	}
	
	public class LogChallengeEventRequest_SAVE : GSTypedRequest<LogChallengeEventRequest_SAVE, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_SAVE() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "SAVE");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_SAVE SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		public LogChallengeEventRequest_SAVE Set_coin( long value )
		{
			request.AddNumber("coin", value);
			return this;
		}			
		public LogChallengeEventRequest_SAVE Set_gem( long value )
		{
			request.AddNumber("gem", value);
			return this;
		}			
	}
	
	public class LogEventRequest_SET_DIARY : GSTypedRequest<LogEventRequest_SET_DIARY, LogEventResponse>
	{
	
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogEventResponse (response);
		}
		
		public LogEventRequest_SET_DIARY() : base("LogEventRequest"){
			request.AddString("eventKey", "SET_DIARY");
		}
		
		public LogEventRequest_SET_DIARY Set_EMOJI_NAME( string value )
		{
			request.AddString("EMOJI_NAME", value);
			return this;
		}
		
		public LogEventRequest_SET_DIARY Set_EMOJI_TYPE( string value )
		{
			request.AddString("EMOJI_TYPE", value);
			return this;
		}
		
		public LogEventRequest_SET_DIARY Set_EMOJI_STATUS( string value )
		{
			request.AddString("EMOJI_STATUS", value);
			return this;
		}
		
		public LogEventRequest_SET_DIARY Set_DATE( string value )
		{
			request.AddString("DATE", value);
			return this;
		}
	}
	
	public class LogChallengeEventRequest_SET_DIARY : GSTypedRequest<LogChallengeEventRequest_SET_DIARY, LogChallengeEventResponse>
	{
		public LogChallengeEventRequest_SET_DIARY() : base("LogChallengeEventRequest"){
			request.AddString("eventKey", "SET_DIARY");
		}
		
		protected override GSTypedResponse BuildResponse (GSObject response){
			return new LogChallengeEventResponse (response);
		}
		
		/// <summary>
		/// The challenge ID instance to target
		/// </summary>
		public LogChallengeEventRequest_SET_DIARY SetChallengeInstanceId( String challengeInstanceId )
		{
			request.AddString("challengeInstanceId", challengeInstanceId);
			return this;
		}
		public LogChallengeEventRequest_SET_DIARY Set_EMOJI_NAME( string value )
		{
			request.AddString("EMOJI_NAME", value);
			return this;
		}
		public LogChallengeEventRequest_SET_DIARY Set_EMOJI_TYPE( string value )
		{
			request.AddString("EMOJI_TYPE", value);
			return this;
		}
		public LogChallengeEventRequest_SET_DIARY Set_EMOJI_STATUS( string value )
		{
			request.AddString("EMOJI_STATUS", value);
			return this;
		}
		public LogChallengeEventRequest_SET_DIARY Set_DATE( string value )
		{
			request.AddString("DATE", value);
			return this;
		}
	}
	
}
	

namespace GameSparks.Api.Messages {


}
