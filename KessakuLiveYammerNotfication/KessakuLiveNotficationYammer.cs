﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceProcess;
using System.Text;
using System.Web.Script.Serialization;
using bExcellent.Service.Util.Domain;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Triggers;

namespace KessakuLiveYammerNotfication
{
    public partial class KessakuLiveNotficationYammer : ServiceBase
    {
        private static IScheduler _scheduler;

        public KessakuLiveNotficationYammer()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            _scheduler = schedulerFactory.GetScheduler();
            _scheduler.Start();
            AddJobs();
        }

        protected override void OnStop()
        {
        }

        public void AddJobs()
        {
            //plan notifcation
            const string cronExpyammer = " 0 0/1 * 1/1 * ? *  ";
            var PlanNotificationJob = new LiveYammerNotoficationJob();
            var PlanNotificationJobDetail = new JobDetailImpl("kessakuLiveYammerNotoficationJob", "kessakuLiveYammerNotoficationJob", PlanNotificationJob.GetType());
            var PlanNotificationtrigger = new CronTriggerImpl("kessakuLiveYammerNotoficationJob", "kessakuLiveYammerNotoficationJob", cronExpyammer);
            _scheduler.ScheduleJob(PlanNotificationJobDetail, PlanNotificationtrigger);
        }

        internal interface ILiveYammerNotoficationJob : IJob
        {
        }

        internal class LiveYammerNotoficationJob : ILiveYammerNotoficationJob
        {
            private bExcellent.Service.BusinessLogic.Common.Common common = new bExcellent.Service.BusinessLogic.Common.Common();

            /// <summary>
            /// Called by the <see cref="T:Quartz.IScheduler" /> when a <see cref="T:Quartz.ITrigger" />
            /// fires that is associated with the <see cref="T:Quartz.IJob" />.
            /// </summary>
            /// <param name="context">The execution context.</param>
            /// <remarks>
            /// The implementation may wish to set a  result object on the
            /// JobExecutionContext before this method exits.  The result itself
            /// is meaningless to Quartz, but may be informative to
            /// <see cref="T:Quartz.IJobListener" />s or
            /// <see cref="T:Quartz.ITriggerListener" />s that are watching the job's
            /// execution.
            /// </remarks>
            public void Execute(IJobExecutionContext context)
            {
                //

                common.SendPlanNotification();
            }
        }

        public static void GetYammerContent()
        {
            Log("GetYammerContent");
            bExcellent.Service.BusinessLogic.Common.Common common = new bExcellent.Service.BusinessLogic.Common.Common();

            var getyammer = new List<YammerContent>(common.GetYammerTopContent());
            foreach (var yammerContent in getyammer)
            {
                if (yammerContent.Groupid != null)
                {
                    SendYammerGroup(yammerContent.ToMailid, yammerContent.Subject, yammerContent.Description,
                                   yammerContent.ImageUrl, yammerContent.YammerToken, yammerContent.Groupid);
                    UpdateYammerContent(yammerContent.ID);
                }
                else
                {
                    SendYammerUser(yammerContent.ToMailid, yammerContent.Subject, yammerContent.Description,
                                   yammerContent.ImageUrl, yammerContent.YammerToken);
                    UpdateYammerContent(yammerContent.ID);
                }
            }
        }

        public static void SendYammerUser(string emailid, string subject, string description, string imageurl, string yammertoken)
        {
            var span = new TimeSpan(DateTime.Now.GetHashCode());
            var timestamp = span.GetHashCode();
            var httpWebRequest =
                (HttpWebRequest)WebRequest.Create("https://www.yammer.com/api/v1/activity.json");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Headers.Set("Authorization", "Bearer " + yammertoken + "");
            httpWebRequest.Method = "POST";
            var cononicalurl = ConfigurationSettings.AppSettings["hostingPrefix"] + "/yammer/BackToSite?id=" + timestamp;
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                var objects = new YammerObject();
                var actor = new YammerActor();
                var activity = new YammerActivity();
                var yammerOuth = new YammerOath();
                objects.url = cononicalurl;
                objects.title = subject;
                objects.image = imageurl;
                objects.description = description;
                actor.email = emailid;
                activity.action = "create";
                activity.message = "";
                activity.Object = objects;
                activity.actor = actor;
                yammerOuth.activity = activity;
                var json = new JavaScriptSerializer().Serialize(yammerOuth);
                var final = json.Replace("Object", "object");

                streamWriter.Write(final);
                streamWriter.Flush();
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                streamReader.ReadToEnd();
            }

            PostOnWall(cononicalurl, description, subject, yammertoken);
        }

        public static void SendYammerGroup(string emailid, string subject, string description, string imageurl, string yammertoken, string groupid)
        {
            Log("SendYammerGroup");
            try
            {
                var span = new TimeSpan(DateTime.Now.GetHashCode());
                var timestamp = span.GetHashCode();
                var httpWebRequest =
                    (HttpWebRequest)WebRequest.Create("https://www.yammer.com/api/v1/activity.json");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Headers.Set("Authorization", "Bearer " + yammertoken + "");
                httpWebRequest.Method = "POST";
                var cononicalurl = ConfigurationManager.AppSettings["HostingPrefix"] + "/yammer/BackToSite?id=" + timestamp;
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    var objects = new YammerObject();
                    var actor = new YammerActor();
                    var activity = new YammerActivity();
                    var yammerOuth = new YammerOath();
                    objects.url = cononicalurl;
                    objects.title = subject;
                    objects.image = imageurl;
                    objects.description = description;
                    actor.email = emailid;
                    activity.action = "create";
                    activity.message = "";
                    activity.Object = objects;
                    activity.actor = actor;
                    yammerOuth.activity = activity;
                    var json = new JavaScriptSerializer().Serialize(yammerOuth);
                    var final = json.Replace("Object", "object");

                    streamWriter.Write(final);
                    streamWriter.Flush();
                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    streamReader.ReadToEnd();
                }

                PostOnGroups(cononicalurl, description, subject, yammertoken, groupid);
            }
            catch (Exception ex)
            {
                Log("SendYammerGroup:" + ex.Message);
            }
        }

        private static void PostOnWall(String cononicalurl, string text, string subject, string token)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://www.yammer.com/api/v1/messages.json");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Headers.Set("Authorization", "Bearer " + token);
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json;
                if (cononicalurl == "")
                {
                    json = "{\"Body\":\"\"}";
                }
                else
                {
                    json = "{\"Body\":\"" + text + "\", \"title\":\" " + subject + "\", \"og_url\":\"" + cononicalurl + "\"}";
                }

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                streamReader.ReadToEnd();
            }
        }

        public static void PostOnGroups(String cononicalurl, string text, string subject, string token, string groupid)
        {
            Log("PostOnGroups:");
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://www.yammer.com/api/v1/messages.json?group_id=" + Convert.ToInt32(groupid));
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Headers.Set("Authorization", "Bearer " + token);
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json;
                if (cononicalurl == "")
                {
                    json = "{\"Body\":\"\"}";
                }
                else
                {
                    json = "{\"Body\":\"" + text + "\",\"group_id\":\"" + groupid + "\",\"og_url\":\"" + cononicalurl + "\",\"og_title\":\"" + text + "\",\"og_description\":\"These are the latest Run DMC shoes. Preorder now\"}";
                }

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            HttpWebResponse httpResponse = null;
            try
            {
                httpResponse = getYammerResponse(httpWebRequest);
            }
            catch (Exception e)
            {
                httpResponse = getYammerResponse(httpWebRequest);
            }
            if (httpResponse != null)
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    streamReader.ReadToEnd();
                }
        }

        private static HttpWebResponse getYammerResponse(HttpWebRequest httpWebRequest)
        {
            HttpWebResponse httpResponse = null;
            try
            {
                httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            }
            catch (Exception ex)
            {
                httpResponse = getYammerResponse(httpWebRequest);
                // throw ex;
            }
            return httpResponse;
        }

        public static void UpdateYammerContent(int id)
        {
            Log("UpdateYammerContent:");
            bExcellent.Service.BusinessLogic.Common.Common common = new bExcellent.Service.BusinessLogic.Common.Common();
            common.UpdateYammercontent(id);
        }

        internal interface IYammerNotificationJob : IJob
        {
        }

        public static void Log(string methodname)
        {
            Trace.Write(Environment.NewLine + Environment.NewLine + "Date :" + DateTime.Now.ToString() + " " + methodname);
        }
    }

    public class YammerObject
    {
        public string url { get; set; }

        public string image { get; set; }

        public string description { get; set; }

        public string title { get; set; }
    }

    public class YammerActor
    {
        public string email { get; set; }
    }

    public class YammerActivity
    {
        public YammerActor actor { get; set; }

        public string action { get; set; }

        public YammerObject Object { get; set; }

        public string message { get; set; }
    }

    public class YammerBody
    {
        public YammerBodyContent Body { get; set; }
    }

    public class YammerBodyContent
    {
        public string og_url { get; set; }

        public string og_title { get; set; }

        public string og_description { get; set; }
    }

    public class YammerOath
    {
        public YammerActivity activity { get; set; }
    }
}