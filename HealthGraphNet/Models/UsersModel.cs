﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace HealthGraphNet.Models
{
    /// <summary>
    /// The user model.
    /// </summary>
	public class UsersModel
    {
        internal const string ContentType = "application/vnd.com.runkeeper.User+json";        
        
        /// <summary>
        /// The unique ID for the user. Read only.
        /// </summary>
		[JsonProperty(PropertyName = "userID")]
        public int UserID { get; set; }

        /// <summary>
        /// The URI of the user profile resource. Read only.
        /// </summary>
		[JsonProperty(PropertyName = "profile")]
        public string Profile { get; set; }

        /// <summary>
        /// The URI of the sharing and display settings resource. Read only.
        /// </summary>
		[JsonProperty(PropertyName = "settings")]
        public string Settings { get; set; }

        /// <summary>
        /// The URI of the first page of the fitness activity feed. Read only.
        /// </summary>
		[JsonProperty(PropertyName = "fitness_activities")]
        public string FitnessActivities { get; set; }

        /// <summary>
        /// The URI of the first page of the strength training activity feed. Read only.
        /// </summary>
		[JsonProperty(PropertyName = "strength_training_activities")]        
        public string StrengthTrainingActivities { get; set; }

        /// <summary>
        /// The URI of the first page of the background activity feed. Read only.
        /// </summary>    
		[JsonProperty(PropertyName = "background_activities")]        
        public string BackgroundActivities { get; set; }

        /// <summary>
        /// The URI of the first page of the sleep feed. Read only.
        /// </summary>     
		[JsonProperty(PropertyName = "sleep")]        
        public string Sleep { get; set; }

        /// <summary>
        /// The URI of the first page of the nutrition feed. Read only.
        /// </summary>     
		[JsonProperty(PropertyName = "nutrition")]        
        public string Nutrition { get; set; }

        /// <summary>
        /// The URI of the first page of the weight measurement feed. Read only.
        /// </summary>     
		[JsonProperty(PropertyName = "weight")]        
        public string Weight { get; set; }

        /// <summary>
        /// The URI of the first page of the general measurements feed. Read only.
        /// </summary>      
		[JsonProperty(PropertyName = "general_measurements")]        
        public string GeneralMeasurements { get; set; }    

        /// <summary>
        /// The URI of the first page of the diabetes measurements feed. Read only.
        /// </summary>     
		[JsonProperty(PropertyName = "diabetes")]        
        public string Diabetes { get; set; }

        /// <summary>
        /// The URI of the personal records resource. Read only.
        /// </summary>      
		[JsonProperty(PropertyName = "records")]        
        public string Records { get; set; } 

        /// <summary>
        /// The URI of the street team resource. Read only.
        /// </summary>      
		[JsonProperty(PropertyName = "team")]        
        public string Team { get; set; }                                    
    }
}