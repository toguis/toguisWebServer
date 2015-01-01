/***********************************************************************************************
 * Project: Tourist Guide System Toguis Web Services
 * University: UNIAJC
 * Authors: Julieth Candia and Carlos Morante
 * Year: 2014 - 2015
 * Version: 1.0 
 * License: GPL V2
 ***********************************************************************************************/
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ToguisController.Utilities;
using ToguisModel;
using System.Data.Linq.SqlClient;
using System.Globalization;

namespace ToguisController.Points
{
    public class PointOfInterestController
    {
        public List<TG_INTEREST_POINT> GetPoints(String login,
                                                 int cityId, 
                                                 bool getMonument, 
                                                 bool getMuseum, 
                                                 bool getHotel, 
                                                 bool getRestaurant,
                                                 bool getInterest,                                       
                                                 bool getBuilding, 
                                                 bool getTransport, 
                                                 bool getEvent,
                                                 int language
                                          )
        {
            List<TG_INTEREST_POINT> loResult =  null;
            using (ToguisEntities loContext = new ToguisEntities())
            {
                try
                {
                    loContext.Configuration.ProxyCreationEnabled = false;
                    List<int> loPoiId = new List<int>();

                    if (getMonument) loPoiId.Add(1);
                    if (getMuseum) loPoiId.Add(2);
                    if (getHotel) loPoiId.Add(3);
                    if (getRestaurant) loPoiId.Add(4);
                    if (getInterest) loPoiId.Add(5);
                    if (getBuilding) loPoiId.Add(6);
                    if (getTransport) loPoiId.Add(7);
                    if (getEvent) loPoiId.Add(8);
                 
                    loResult = (from item in loContext.TG_INTEREST_POINT
                                    where item.CITY_ID == cityId && 
                                          loPoiId.Contains(item.PTYP_ID)
                                    select item).ToList<TG_INTEREST_POINT>();
 
                    foreach (var item in loResult)
                    {
                        loContext.Entry(item).State = EntityState.Detached;
                        List<TG_POI_USER_DATA> loUserData = loContext.TG_POI_USER_DATA.Where(p => p.USR_ID.Equals(login) && p.POI_ID == item.POI_ID).ToList<TG_POI_USER_DATA>();
                        item.TG_POI_USER_DATA = loUserData;

                        List<TG_POI_DESCRIPTION> loDescription = loContext.TG_POI_DESCRIPTION.Where(p => p.LNG_ID == language && p.POI_ID == item.POI_ID).ToList<TG_POI_DESCRIPTION>();
                        item.TG_POI_DESCRIPTION = loDescription;
                        item.RATING = 0;
                    }
                                     
                }
                catch (Exception ex)
                {
                }
            }
            return loResult;            
        }

        public List<TG_INTEREST_POINT> GetPointsWithDistance(String login,
                                                 int cityId,
                                                 bool getMonument,
                                                 bool getMuseum,
                                                 bool getHotel,
                                                 bool getRestaurant,
                                                 bool getInterest,
                                                 bool getBuilding,
                                                 bool getTransport,
                                                 bool getEvent,
                                                 int language,
                                                 double userLatitude,
                                                 double userLongitude,
                                                 double maxDistance
                                          )
        {
            List<TG_INTEREST_POINT> loPoints = this.GetPoints(login, cityId, getMonument, getMuseum, getHotel, getRestaurant, getInterest, getBuilding, getTransport, getEvent, language);
            List<TG_INTEREST_POINT> loResult = new List<TG_INTEREST_POINT>();
            if (maxDistance > 0)
            {
                foreach (var item in loPoints)
                {
                    double pointDistanceToUser = HaversineUtility.GetDistance(userLatitude, userLongitude, item.POI_LATITUDE, item.POI_LONGITUDE);
                    if (pointDistanceToUser <= maxDistance)
                    {
                        loResult.Add(item);
                    }
                }
            }
            else
            {
                loResult = loPoints;
            }

            return loResult;
        }

        public List<TG_INTEREST_POINT> SearchPoints(String login,
                                                 int cityId,
                                                 bool getMonument,
                                                 bool getMuseum,
                                                 bool getHotel,
                                                 bool getRestaurant,
                                                 bool getInterest,
                                                 bool getBuilding,
                                                 bool getTransport,
                                                 bool getEvent,
                                                 int language,
                                                 double userLatitude,
                                                 double userLongitude,
                                                 double maxDistance,
                                                 string search)
        {
            List<TG_INTEREST_POINT> loPoints = GetPointsWithDistance(login, cityId, getMonument, getMuseum, getHotel, getRestaurant, getInterest, getBuilding, getTransport, getEvent, language, userLatitude, userLongitude, maxDistance);
            List<TG_INTEREST_POINT> loResult = (from item in loPoints
                                                where item.TG_POI_DESCRIPTION.FirstOrDefault().POID_NAME.ToLower().Contains(search.Trim().ToLower())
                                                select item).ToList();
            return loResult;
        }

        public TG_INTEREST_POINT GetPoint(String login, string poiId, string language, string pPath)
        {
            TG_INTEREST_POINT loResult = null;
            using (ToguisEntities loContext = new ToguisEntities())
            {
                try
                {
                    int liPoiId = int.Parse(poiId);
                    int liLanguage = int.Parse(language);
                    loContext.Configuration.ProxyCreationEnabled = false;
                    loResult = (from item in loContext.TG_INTEREST_POINT
                                where item.POI_ID == liPoiId
                                select item).FirstOrDefault();
                    
                    loContext.Entry(loResult).State = EntityState.Detached;
                    try
                    {
                        var loRating = (from item in loContext.TG_POI_USER_DATA
                                        where item.POI_ID == liPoiId &&
                                              item.UDAT_RATING != null
                                        group item by item.POI_ID into d
                                        select new { SumItems = d.Sum(p => p.UDAT_RATING) / d.Count() }).FirstOrDefault();

                        loResult.RATING = (float)loRating.SumItems;
                    }
                    catch (Exception)
                    {
                    }


                    List<TG_POI_USER_DATA> loUserData = loContext.TG_POI_USER_DATA.Where(p => p.USR_ID.Equals(login) && p.POI_ID == loResult.POI_ID).ToList<TG_POI_USER_DATA>();
                    loResult.TG_POI_USER_DATA = loUserData;

                    List<TG_POI_DESCRIPTION> loDescription = loContext.TG_POI_DESCRIPTION.Where(p => p.LNG_ID == liLanguage && p.POI_ID == loResult.POI_ID).ToList<TG_POI_DESCRIPTION>();
                    loResult.TG_POI_DESCRIPTION = loDescription;

                    String lsImageData = ConvertImage.ConvertImageToBase64(pPath + @"\App_Data\files\" + loResult.POI_IMAGE_PATH);
                    loResult.POI_IMAGE_PATH = lsImageData;
                }
                catch (Exception ex)
                {
                }
            }
            return loResult;            

        }

        public List<TG_COMMENTS> GetComments(string poiId)
        {
            List<TG_COMMENTS> loResult = new List<TG_COMMENTS>();
            using (ToguisEntities loContext = new ToguisEntities())
            {
                loContext.Configuration.ProxyCreationEnabled = false;
                try
                {
                    int liPoiId = int.Parse(poiId);
                    loResult = (from item in loContext.TG_COMMENTS
                                where item.POI_ID == liPoiId
                                orderby item.COM_DATE ascending
                                select item
                               ).ToList();

                    foreach (var item in loResult)
                    {
                        loContext.Entry(item).State = EntityState.Detached;
                        TG_USER loUser = (from user in loContext.TG_USER
                                          where user.USR_ID.Equals(item.USR_ID)
                                          select user).FirstOrDefault();
                        item.TG_USER = loUser;
                    }
                }
                catch (Exception ex) 
                {

                }
            }
            return loResult;
        }

        public int SetComment(TG_COMMENTS comment)
        {

            using (ToguisEntities loContext = new ToguisEntities())
            {
                try
                {
                    loContext.TG_COMMENTS.Add(comment);
                    loContext.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    return 1;
                }
                catch (Exception ex)
                {
                    return 2;
                }
            }
            return 0;
        }

        public TG_COMMENTS GetComment(int commentId)
        {
           TG_COMMENTS loResult = null;
           using (ToguisEntities loContext = new ToguisEntities())
            {
                loContext.Configuration.ProxyCreationEnabled = false;
                try
                {
                    loResult = (from item in loContext.TG_COMMENTS
                                where item.COM_ID == commentId
                                select item).FirstOrDefault();
                }

                catch (Exception ex)
                {
                }
            }
           return loResult;
        }

        public int SetRating(String login, string poiId, string rating)
        {
            using (ToguisEntities loContext = new ToguisEntities())
            {
                try
                {
                    int liPoiId = int.Parse(poiId);
                    char loSeparator = Convert.ToChar(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
                    String lsRating = rating.Contains(".") ? rating.Replace('.', loSeparator) : rating.Replace(',', loSeparator);
                    float lfRating = float.Parse(lsRating);

                    TG_POI_USER_DATA loUserData = loContext.TG_POI_USER_DATA.Where(p => p.POI_ID == liPoiId && p.USR_ID.Equals(login)).FirstOrDefault();
                    if (loUserData != null)
                    {
                        loUserData.UDAT_RATING = lfRating;
                    }
                    else
                    {
                        loUserData = new TG_POI_USER_DATA();
                        loUserData.POI_ID = liPoiId;
                        loUserData.USR_ID = login;
                        loUserData.UDAT_FAVORITE = false;
                        loUserData.UDAT_VISITED = false;
                        loUserData.UDAT_RATING = lfRating;
                        loContext.TG_POI_USER_DATA.Add(loUserData);
                    }
                    
                    loContext.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    return 1;
                }
                catch (Exception ex)
                {
                    return 2;
                }
            }
            return 0;      
        }

        public float GetRating(String login, string poiId)
        {
            float lfResult = -1f;
            using (ToguisEntities loContext = new ToguisEntities())
            {
                loContext.Configuration.ProxyCreationEnabled = false;
                try
                {
                    int liPoiId = int.Parse(poiId);
                    TG_POI_USER_DATA loUserData = loContext.TG_POI_USER_DATA.Where(p => p.POI_ID == liPoiId && p.USR_ID.Equals(login)).FirstOrDefault();
                    lfResult = (float)loUserData.UDAT_RATING;
                }
                catch (Exception ex)
                {
                }
            }
            return lfResult;   
        }

        public int SetFavorite(String login, string poiId, string value)
        {
            using (ToguisEntities loContext = new ToguisEntities())
            {
                try
                {
                    int liPoiId = int.Parse(poiId);
                    bool lboValue = bool.Parse(value);
                    TG_POI_USER_DATA loUserData = loContext.TG_POI_USER_DATA.Where(p => p.POI_ID == liPoiId && p.USR_ID.Equals(login)).FirstOrDefault();
                    if (loUserData != null)
                    {
                        loUserData.UDAT_FAVORITE = Convert.ToBoolean(value);
                    }
                    else
                    {
                        loUserData = new TG_POI_USER_DATA();
                        loUserData.POI_ID = liPoiId;
                        loUserData.USR_ID = login;
                        loUserData.UDAT_FAVORITE = lboValue;
                        loUserData.UDAT_VISITED = false;
                        loUserData.UDAT_RATING = 0.0;
                        loContext.TG_POI_USER_DATA.Add(loUserData);
                    }

                    loContext.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    return 1;
                }
                catch (Exception ex)
                {
                    return 2;
                }
            }
            return 0;              
        }

        public int SetVisited(String login, string poiId, string value)
        {
            using (ToguisEntities loContext = new ToguisEntities())
            {
                try
                {
                    int liPoiId = int.Parse(poiId);
                    bool lboValue = bool.Parse(value);
                    TG_POI_USER_DATA loUserData = loContext.TG_POI_USER_DATA.Where(p => p.POI_ID == liPoiId && p.USR_ID.Equals(login)).FirstOrDefault();
                    if (loUserData != null)
                    {
                        loUserData.UDAT_VISITED = Convert.ToBoolean(value);
                    }
                    else
                    {
                        
                        loUserData = new TG_POI_USER_DATA();
                        loUserData.POI_ID = liPoiId;
                        loUserData.USR_ID = login;
                        loUserData.UDAT_FAVORITE = false;
                        loUserData.UDAT_VISITED = lboValue;
                        loUserData.UDAT_RATING = 0.0;
                        loContext.TG_POI_USER_DATA.Add(loUserData);
                    }

                    loContext.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    return 1;
                }
                catch (Exception ex)
                {
                    return 2;
                }
            }
            return 0;
        }


        public List<TG_LANGUAGE> GetLanguages()
        {
            List<TG_LANGUAGE> loResult = new List<TG_LANGUAGE>();
            using (ToguisEntities loContext = new ToguisEntities())
            {
                loContext.Configuration.ProxyCreationEnabled = false;
                try
                {
                    loResult = (from item in loContext.TG_LANGUAGE
                               select item).ToList();
                }

                catch (Exception ex)
                {
                }
            }
            return loResult;
        }

        public List<TG_CITY> GetCities()
        {
            List<TG_CITY> loResult = new List<TG_CITY>();
            using (ToguisEntities loContext = new ToguisEntities())
            {
                loContext.Configuration.ProxyCreationEnabled = false;
                try
                {
                    loResult = (from item in loContext.TG_CITY
                                select item).ToList();
                }

                catch (Exception ex)
                {
                }
            }
            return loResult;
        }

    }
}
